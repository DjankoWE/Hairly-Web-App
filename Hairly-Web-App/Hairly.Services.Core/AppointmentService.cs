using Hairly.Data;
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
    }
}
