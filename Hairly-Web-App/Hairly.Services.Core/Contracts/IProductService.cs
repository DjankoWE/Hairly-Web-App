using Hairly.Web.ViewModels.Product;

namespace Hairly.Services.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync();
    }
}
