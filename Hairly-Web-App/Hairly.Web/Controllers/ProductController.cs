using Hairly.Services.Core.Contracts;
using Hairly.Web.Helpers;
using Hairly.Web.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Hairly.Web.Controllers
{
    public class ProductController : Controller
    {
        private const int PageSize = 9;
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            var products = await productService.GetAllProductsAsync();
            var paginatedProducts = PaginatedList<ProductIndexViewModel>
                .Create(products.ToList(), pageNumber, PageSize);

            return View(paginatedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
