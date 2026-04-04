using Hairly.Web.ViewModels.Product;

namespace Hairly.Services.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync();

        Task<ProductDetailsViewModel?> GetProductByIdAsync(int id);

        Task<ProductCreateViewModel> GetProductCreateModelAsync();

        Task<bool> CreateProductAsync(ProductCreateViewModel model);

        Task<ProductEditViewModel?> GetProductEditModelAsync(int id);

        Task<bool> UpdateProductAsync(ProductEditViewModel model);
    }
}
