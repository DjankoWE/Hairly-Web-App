using Hairly.Web.ViewModels.UserAppointment;

namespace Hairly.Services.Core.Contracts
{
    public interface IUserAppointmentService
    {
        Task<IEnumerable<UserAppointmentIndexViewModel>> GetMyAppointmentsAsync(string userId);

        Task<UserAppointmentBookViewModel> GetBookingModelAsync();

        Task<bool> BookAppointmentAsync(UserAppointmentBookViewModel model, string userId);

        Task<int> GetOrCreateClientIdForUserAsync(string userId);
    }
}
