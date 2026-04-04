using Hairly.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hairly.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();
            return View(products);
        }
    }
}
