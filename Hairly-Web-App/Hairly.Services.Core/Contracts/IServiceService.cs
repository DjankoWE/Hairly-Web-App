using Hairly.Web.ViewModels.Service;

namespace Hairly.Services.Core.Contracts
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceIndexViewModel>> GetAllServicesAsync(string hairdresserId);
    }
}
