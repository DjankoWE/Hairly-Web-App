using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hairly.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ApplicationDbContext context;
        private IProductService productService;
        private Product testProduct;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            SeedTestData();
            productService = new ProductService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private void SeedTestData()
        {
            testProduct = new Product
            {
                Id = 1,
                Name = "Hair Shampoo",
                Description = "Professional shampoo",
                Price = 25.00m,
                QuantityInStock = 10,
                ImageUrl = "/images/shampoo.jpg",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Products.Add(testProduct);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllProductsAsync_ReturnsProducts()
        {
            var result = await productService.GetAllProductsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetProductByIdAsync_ValidId_ReturnsProduct()
        {
            var result = await productService.GetProductByIdAsync(testProduct.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetProductByIdAsync_InvalidId_ReturnsNull()
        {
            var result = await productService.GetProductByIdAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetProductCreateModelAsync_ReturnsModel()
        {
            var result = await productService.GetProductCreateModelAsync();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateProductAsync_ValidData_ReturnsTrue()
        {
            var model = new ProductCreateViewModel
            {
                Name = "Hair Conditioner",
                Description = "Professional conditioner",
                Price = 30.00m,
                QuantityInStock = 15,
                ImageUrl = "/images/conditioner.jpg"
            };

            var result = await productService.CreateProductAsync(model);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetProductEditModelAsync_ValidId_ReturnsModel()
        {
            var result = await productService.GetProductEditModelAsync(testProduct.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetProductEditModelAsync_InvalidId_ReturnsNull()
        {
            var result = await productService.GetProductEditModelAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateProductAsync_ValidData_ReturnsTrue()
        {
            var model = new ProductEditViewModel
            {
                Id = testProduct.Id,
                Name = "Updated Shampoo",
                Description = "Updated description",
                Price = 28.00m,
                QuantityInStock = 12,
                ImageUrl = "/images/shampoo.jpg"
            };

            var result = await productService.UpdateProductAsync(model);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateProductAsync_InvalidId_ReturnsFalse()
        {
            var model = new ProductEditViewModel
            {
                Id = 999,
                Name = "Test",
                Description = "Test",
                Price = 10.00m,
                QuantityInStock = 5,
                ImageUrl = "/test.jpg"
            };

            var result = await productService.UpdateProductAsync(model);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetProductDeleteModelAsync_ValidId_ReturnsModel()
        {
            var result = await productService.GetProductDeleteModelAsync(testProduct.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetProductDeleteModelAsync_InvalidId_ReturnsNull()
        {
            var result = await productService.GetProductDeleteModelAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteProductAsync_ValidId_ReturnsTrue()
        {
            var result = await productService.DeleteProductAsync(testProduct.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteProductAsync_InvalidId_ReturnsFalse()
        {
            var result = await productService.DeleteProductAsync(999);

            Assert.IsFalse(result);
        }
    }
}