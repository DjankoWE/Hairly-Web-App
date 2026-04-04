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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await productService.GetProductCreateModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isCreated = await productService.CreateProductAsync(model);

            if (isCreated)
            {
                TempData[SuccessMessageKey] = ProductCreatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ProductCreateError);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await productService.GetProductEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isUpdated = await productService.UpdateProductAsync(model);

            if (isUpdated)
            {
                TempData[SuccessMessageKey] = ProductUpdatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ProductUpdateError);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await productService.GetProductDeleteModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool isDeleted = await productService.DeleteProductAsync(id);

            if (isDeleted)
            {
                TempData[SuccessMessageKey] = ProductDeletedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            TempData[ErrorMessageKey] = ProductDeleteError;
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}
