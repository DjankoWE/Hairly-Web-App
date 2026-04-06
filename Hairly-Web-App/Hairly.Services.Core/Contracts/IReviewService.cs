using Hairly.Web.ViewModels.Review;

namespace Hairly.Services.Core.Contracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewIndexViewModel>> GetAllReviewsAsync();

        Task<ReviewDetailsViewModel?> GetReviewByIdAsync(int id);

        Task<ReviewCreateViewModel?> GetReviewCreateModelAsync(int appointmentId, string userId);

        Task<bool> CreateReviewAsync(ReviewCreateViewModel model, string userId);
    }
}
