using Hairly.Web.ViewModels.UserAppointment;

namespace Hairly.Services.Core.Contracts
{
    public interface IUserAppointmentService
    {
        Task<IEnumerable<UserAppointmentIndexViewModel>> GetMyAppointmentsAsync(string userId);
    }
}
