using Hairly.Web.ViewModels.Appointment;

namespace Hairly.Services.Core.Contracts
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentIndexViewModel>> GetAllAppointmentsAsync(string hairdresserId);

        Task<AppointmentCreateViewModel> GetAppointmentCreateModelAsync(string hairdresserId);

        Task<bool> CreateAppointmentAsync(AppointmentCreateViewModel viewModel, string hairdresserId);

        Task<AppointmentEditViewModel?> GetAppointmentForEditAsync(int id, string hairdresserId);

        Task<bool> UpdateAppointmentAsync(AppointmentEditViewModel viewModel, string hairdresserId);
    }
}
