using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace Hairly.Web.Controllers
{
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string hairdresserId = GetUserId();
            var appointments = await appointmentService.GetAllAppointmentsAsync(hairdresserId);

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string hairdresserId = GetUserId();
            var viewModel = await appointmentService.GetAppointmentCreateModelAsync(hairdresserId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                string hairdresserId = GetUserId();
                var reloadedModel = await appointmentService.GetAppointmentCreateModelAsync(hairdresserId);

                viewModel.Clients = reloadedModel.Clients;
                viewModel.Services = reloadedModel.Services;

                return View(viewModel);
            }

            string userId = GetUserId();
            bool isCreated = await appointmentService.CreateAppointmentAsync(viewModel, userId);

            if (isCreated)
            {
                TempData["SuccessMessage"] = "Appointment created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while creating the appointment. Please try again.");
            return View(viewModel);
        }
    }
}
