using Hairly.Data.Models.Enums;
using Hairly.Services.Core.Contracts;
using Hairly.Web.Helpers;
using Hairly.Web.ViewModels.Appointment;
using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;
using static Hairly.GCommon.ApplicationConstants.SuccessMessages;

namespace Hairly.Web.Controllers
{
    public class AppointmentController : BaseController
    {
        private const int PageSize = 10;
        private readonly IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string status = null, string clientSearch = null, int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;

            }

            string hairdresserId = GetUserId();
            var appointments = await appointmentService.GetAllAppointmentsAsync(hairdresserId);

            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<AppointmentStatus>(status, out var statusEnum))
                {
                    appointments = appointments.Where(a => a.Status == statusEnum);
                }
            }

            if (!string.IsNullOrEmpty(clientSearch))
            {
                appointments = appointments.Where(a => a.ClientFullName.Contains(clientSearch, StringComparison.OrdinalIgnoreCase));
            }

            var paginatedAppointments = PaginatedList<AppointmentIndexViewModel>
                    .Create(appointments.ToList(), pageNumber, PageSize);

            ViewData["CurrentStatus"] = status;
            ViewData["CurrentClientSearch"] = clientSearch;

            return View(paginatedAppointments);
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
                TempData[SuccessMessageKey] = AppointmentCreatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            string errorHairdresserId = GetUserId();
            var errorReloadedModel = await appointmentService.GetAppointmentCreateModelAsync(errorHairdresserId);

            viewModel.Clients = errorReloadedModel.Clients;
            viewModel.Services = errorReloadedModel.Services;

            ModelState.AddModelError(string.Empty, AppointmentCreateError);
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
                TempData[SuccessMessageKey] = AppointmentUpdatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, AppointmentUpdateError);
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
                TempData[SuccessMessageKey] = AppointmentDeletedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            TempData[ErrorMessageKey] = AppointmentDeleteError;
            return RedirectToAction(nameof(Index));
        }
    }
}
