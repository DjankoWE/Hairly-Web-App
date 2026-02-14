using Hairly.Web.ViewModels.Client;

namespace Hairly.Services.Core.Contracts
{
    public interface IClientService
    {
        Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync(string? hairdresserId);
    }
}
