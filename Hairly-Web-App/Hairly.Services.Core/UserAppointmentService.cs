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
    }
}