using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hairly.Web.Controllers
{
    public class ServiceController : BaseController
    {
        private readonly IServiceService serviceService;

        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string hairdresserId = GetUserId();
            var services = await serviceService.GetAllServicesAsync(hairdresserId);

            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string hairdresserId = GetUserId();
            bool isCreated = await serviceService.CreateServiceAsync(viewModel, hairdresserId);

            if (isCreated)
            {
                TempData["SuccessMessage"] = "Service created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the service. Please try again.");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string hairdresserId = GetUserId();
            var service = await serviceService.GetServiceForEditAsync(id, hairdresserId);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string hairdresserId = GetUserId();
            bool isUpdated = await serviceService.UpdateServiceAsync(viewModel, hairdresserId);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Service updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the service. Please try again.");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string hairdresserId = GetUserId();
            var service = await serviceService.GetServiceForDeleteAsync(id, hairdresserId);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string hairdresserId = GetUserId();
            bool isDeleted = await serviceService.DeleteServiceAsync(id, hairdresserId);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Service deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the service. Please try again.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
