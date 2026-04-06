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

        public async Task<ReviewCreateViewModel?> GetReviewCreateModelAsync(int appointmentId, string userId)
        {
            var appointment = await dbContext.Appointments
                .Include(a => a.Client)
                .Include(a => a.Service)
                .Include(a => a.Hairdresser)
                .FirstOrDefaultAsync(a => a.Id == appointmentId);

            if (appointment == null || appointment.Client.UserId != userId || appointment.Status != AppointmentStatus.Completed)
            {
                return null;
            }

            var existingReview = await dbContext.Reviews
                .AnyAsync(r => r.AppointmentId == appointmentId);

            if (existingReview)
            {
                return null;
            }

            return new ReviewCreateViewModel
            {
                AppointmentId = appointment.Id,
                ServiceName = appointment.Service.Name,
                HairdresserName = appointment.Hairdresser.UserName ?? "Unknown",
                AppointmentDate = appointment.AppointmentDate
            };
        }

        public async Task<bool> CreateReviewAsync(ReviewCreateViewModel model, string userId)
        {
            try
            {
                var appointment = await dbContext.Appointments
                    .Include(a => a.Client)
                    .FirstOrDefaultAsync(a => a.Id == model.AppointmentId);

                if (appointment == null || appointment.Client.UserId != userId || appointment.Status != AppointmentStatus.Completed)
                {
                    return false;
                }

                var existingReview = await dbContext.Reviews
                    .AnyAsync(r => r.AppointmentId == model.AppointmentId);

                if (existingReview)
                {
                    return false;
                }

                var review = new Review
                {
                    ClientId = appointment.ClientId,
                    AppointmentId = model.AppointmentId,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                await dbContext.Reviews.AddAsync(review);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
