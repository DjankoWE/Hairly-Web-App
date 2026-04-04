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
    }
}
