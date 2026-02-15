using Hairly.Web.ViewModels.Client;

namespace Hairly.Services.Core.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync(string hairdresserId);

        Task<bool> CreateClientAsync(ClientCreateViewModel viewModel, string hairdresserId);

        Task<ClientEditViewModel?> GetClientForEditAsync(int id, string hairdresserId);

        Task<bool> UpdateClientAsync(ClientEditViewModel viewModel, string hairdresserId);

        Task<ClientDetailsViewModel> GetClientDetailsAsync(int id, string hairdresserId);

        Task<ClientDeleteViewModel> GetClientForDeleteAsync(int id, string hairdresserId);

        Task<bool> DeleteClientAsync(int id, string hairdresserId);
    }
}
