using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.UserAppointment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Hairly.GCommon.ApplicationConstants;

namespace Hairly.Services.Core
{
    public class UserAppointmentService : IUserAppointmentService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public UserAppointmentService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserAppointmentIndexViewModel>> GetMyAppointmentsAsync(string userId)
        {
            return await dbContext.Appointments
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Include(a => a.Hairdresser)
                .Include(a => a.Reviews)
                .Where(a => a.Client.UserId == userId)
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new UserAppointmentIndexViewModel
                {
                    Id = a.Id,
                    ServiceName = a.Service.Name,
                    HairdresserName = a.Hairdresser.UserName ?? "Unknown",
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    HasReview = a.Reviews.Any()
                })
                .ToListAsync();
        }

        public async Task<UserAppointmentBookViewModel> GetBookingModelAsync()
        {
            var services = await dbContext.Services
                .Where(s => !s.IsDeleted)
                .Select(s => new ServiceSelectViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    DurationInMinutes = s.DurationInMinutes
                })
                .ToListAsync();

            return new UserAppointmentBookViewModel
            {
                AvailableServices = services
            };
        }

        public async Task<bool> BookAppointmentAsync(UserAppointmentBookViewModel model, string userId)
        {
            try
            {
                var clientId = await GetOrCreateClientIdForUserAsync(userId);

                var service = await dbContext.Services.FirstOrDefaultAsync(s => s.Id == model.ServiceId);

                if (service == null)
                {
                    return false;
                }

                var appointment = new Appointment
                {
                    ClientId = clientId,
                    ServiceId = model.ServiceId,
                    HairdresserId = service.HairdresserId,
                    AppointmentDate = model.AppointmentDate,
                    Status = AppointmentStatus.Scheduled,
                    Note = model.Note,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                await dbContext.Appointments.AddAsync(appointment);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetOrCreateClientIdForUserAsync(string userId)
        {
            var existingClient = await dbContext.Clients
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (existingClient != null)
            {
                return existingClient.Id;
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("User not found!");
            }

            var claims = await userManager.GetClaimsAsync(user);
            var firstName = claims.FirstOrDefault(c => c.Type == "FirstName")?.Value ?? "User";
            var lastName = claims.FirstOrDefault(c => c.Type == "LastName")?.Value ?? user.Email?.Split('@').First() ?? "Client";

            var hairdresser = await userManager.GetUsersInRoleAsync(HairdresserRoleName);
            var hairdresserId = hairdresser.FirstOrDefault()?.Id 
                                ?? (await userManager.GetUsersInRoleAsync(AdminRoleName)).FirstOrDefault()?.Id 
                                ?? throw new InvalidOperationException("No hairdresser found!");

            var client = new Client
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = user.PhoneNumber ?? "N/A",
                Email = user.Email,
                UserId = userId,
                HairdresserId = hairdresserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();

            return client.Id;
        }
    }
}