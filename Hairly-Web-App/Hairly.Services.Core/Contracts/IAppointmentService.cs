using Hairly.Web.ViewModels.Appointment;

namespace Hairly.Services.Core.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentIndexViewModel>> GetAllAppointmentsAsync(string hairdresserId);

        Task<AppointmentCreateViewModel> GetAppointmentCreateModelAsync(string hairdresserId);

        Task<bool> CreateAppointmentAsync(AppointmentCreateViewModel viewModel, string hairdresserId);
    }
}
