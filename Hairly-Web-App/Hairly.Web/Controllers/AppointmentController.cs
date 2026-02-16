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

            string errorHairdresserId = GetUserId();
            var errorReloadedModel = await appointmentService.GetAppointmentCreateModelAsync(errorHairdresserId);

            viewModel.Clients = errorReloadedModel.Clients;
            viewModel.Services = errorReloadedModel.Services;

            ModelState.AddModelError(string.Empty, "An error occurred while creating the appointment. Please try again.");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string hairdresserId = GetUserId();
            var viewModel = await appointmentService.GetAppointmentForEditAsync(id, hairdresserId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                string hairdresserId = GetUserId();

                var reloadedModel = await appointmentService.GetAppointmentForEditAsync(id, hairdresserId);

                if (reloadedModel != null)
                {
                    viewModel.Clients = reloadedModel.Clients;
                    viewModel.Services = reloadedModel.Services;
                }

                return View(viewModel);
            }

            string userId = GetUserId();
            bool isUpdated = await appointmentService.UpdateAppointmentAsync(viewModel, userId);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Appointment updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty,
                "An error occurred while updating the appointment. Please try again.");
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string hairdresserId = GetUserId();
            var viewModel = await appointmentService.GetAppointmentDetailsAsync(id, hairdresserId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string hairdresserId = GetUserId();
            var viewModel = await appointmentService.GetAppointmentForDeleteAsync(id, hairdresserId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string hairdresserId = GetUserId();
            bool isDeleted = await appointmentService.DeleteAppointmentAsync(id, hairdresserId);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Appointment deleted successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "An error occurred while deleting the appointment. Please try again.";
            return RedirectToAction(nameof(Index));
        }
    }
}
