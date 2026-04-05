using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Services.Core
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ReviewIndexViewModel>> GetAllReviewsAsync()
        {
            return await dbContext.Reviews
                .Include(r => r.Client)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Service)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Hairdresser)
                .Select(r => new ReviewIndexViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn,
                    ClientName = $"{r.Client.FirstName} {r.Client.LastName}",
                    HairdresserName = r.Appointment.Hairdresser.UserName ?? "Unknown",
                    ServiceName = r.Appointment.Service.Name
                })
                .OrderByDescending(r => r.CreatedOn)
                .ToListAsync();
        }

        public async Task<ReviewDetailsViewModel?> GetReviewByIdAsync(int id)
        {
            return await dbContext.Reviews
                .Include(r => r.Client)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Service)
                .Include(r => r.Appointment)
                    .ThenInclude(a => a.Hairdresser)
                .Where(r => r.Id == id)
                .Select(r => new ReviewDetailsViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn,
                    ClientName = $"{r.Client.FirstName} {r.Client.LastName}",
                    HairdresserName = r.Appointment.Hairdresser.UserName ?? "Unknown",
                    ServiceName = r.Appointment.Service.Name,
                    AppointmentDate = r.Appointment.AppointmentDate
                })
                .FirstOrDefaultAsync();
        }
    }
}
