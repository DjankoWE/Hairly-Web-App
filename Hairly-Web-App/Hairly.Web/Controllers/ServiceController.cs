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
    }
}
