using Hairly.Web.ViewModels.Service;

namespace Hairly.Services.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceIndexViewModel>> GetAllServicesAsync(string hairdresserId);

        Task<bool> CreateServiceAsync(ServiceCreateViewModel viewModel, string hairdresserId);

        Task<ServiceEditViewModel?> GetServiceForEditAsync(int id, string hairdresserId);

        Task<bool> UpdateServiceAsync(ServiceEditViewModel viewModel, string hairdresserId);
    }
}
