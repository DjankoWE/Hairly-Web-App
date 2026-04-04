using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Services.Core
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync()
        {
            return await dbContext.Products
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    ImageUrl = p.ImageUrl,
                    IsDeleted = p.IsDeleted
                })
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProductDetailsViewModel?> GetProductByIdAsync(int id)
        {
            return await dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    ImageUrl = p.ImageUrl,
                    CreatedOn = p.CreatedOn
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductCreateViewModel> GetProductCreateModelAsync()
        {
            return await Task.FromResult(new ProductCreateViewModel());
        }

        public async Task<bool> CreateProductAsync(ProductCreateViewModel model)
        {
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    QuantityInStock = model.QuantityInStock,
                    ImageUrl = model.ImageUrl,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductEditViewModel?> GetProductEditModelAsync(int id)
        {
            return await dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductEditViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock,
                    ImageUrl = p.ImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProductAsync(ProductEditViewModel model)
        {
            try
            {
                var product = await dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

                if (product == null)
                {
                    return false;
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.QuantityInStock = model.QuantityInStock;
                product.ImageUrl = model.ImageUrl;

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductDeleteViewModel?> GetProductDeleteModelAsync(int id)
        {
            return await dbContext.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDeleteViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    QuantityInStock = p.QuantityInStock
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    return false;
                }

                product.IsDeleted = true;
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
