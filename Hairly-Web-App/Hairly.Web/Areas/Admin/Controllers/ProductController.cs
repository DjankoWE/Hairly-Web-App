using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;
using static Hairly.GCommon.ApplicationConstants.SuccessMessages;

namespace Hairly.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    [Route("Admin/[controller]/[action]")]
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
