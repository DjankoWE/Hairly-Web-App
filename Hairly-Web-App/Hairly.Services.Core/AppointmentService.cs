using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Appointment;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Services.Core
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext dbContext;

        public AppointmentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<AppointmentIndexViewModel>> GetAllAppointmentsAsync(string hairdresserId)
        {
            return await dbContext.Appointments
                .Where(a => a.HairdresserId == hairdresserId && !a.IsDeleted)
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Select(a => new AppointmentIndexViewModel
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    ClientFullName = $"{a.Client.FirstName} {a.Client.LastName}",
                    ServiceName = a.Service.Name,
                    ServicePrice = a.Service.Price,
                    ServiceDuration = a.Service.DurationInMinutes,
                    Status = a.Status
                })
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();
        }

        public async Task<AppointmentCreateViewModel> GetAppointmentCreateModelAsync(string hairdresserId)
        {
            var clientsList = await dbContext.Clients
                .Where(c => c.HairdresserId == hairdresserId && !c.IsDeleted)
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToListAsync();

            var clients = clientsList
                .ToDictionary
                (
                    c => c.Id,
                    c => $"{c.FirstName} {c.LastName}"
                );

            var servicesList = await dbContext.Services
                .Where(s => s.HairdresserId == hairdresserId && !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToListAsync();

            var services = servicesList
                .ToDictionary
                (
                    s => s.Id,
                    s => s.Name
                );

            return new AppointmentCreateViewModel
            {
                Clients = clients,
                Services = services,
                AppointmentDate = DateTime.Now.AddDays(1).Date.AddHours(10) // This is default for tomorrow at 9 AM
            };
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentCreateViewModel viewModel, string hairdresserId)
        {
            try
            {
                var clientExists = await dbContext.Clients
                    .AnyAsync(c => c.Id == viewModel.ClientId && c.HairdresserId == hairdresserId && !c.IsDeleted);

                var serviceExists = await dbContext.Services
                    .AnyAsync(s => s.Id == viewModel.ServiceId && s.HairdresserId == hairdresserId && !s.IsDeleted);

                if (!clientExists || !serviceExists)
                {
                    return false;
                }

                var appointment = new Appointment
                {
                    AppointmentDate = viewModel.AppointmentDate,
                    ClientId = viewModel.ClientId,
                    ServiceId = viewModel.ServiceId,
                    HairdresserId = hairdresserId,
                    Note = viewModel.Note,
                    Status = AppointmentStatus.Scheduled,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                };

                await dbContext.Appointments.AddAsync(appointment);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
