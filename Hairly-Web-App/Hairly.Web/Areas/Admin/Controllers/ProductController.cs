using Hairly.Services.Core.Contracts;
using Hairly.Web.Helpers;
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
    [Route("Admin/[controller]")]
    public class ProductController : Controller
    {
        private const int PageSize = 10;
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [HttpGet("Index")]
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

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var model = await productService.GetProductCreateModelAsync();
            return View(model);
        }

        [HttpPost("Create")]
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

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await productService.GetProductEditModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost("Edit/{id}")]
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

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await productService.GetProductDeleteModelAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost("Delete/{id}")]
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
