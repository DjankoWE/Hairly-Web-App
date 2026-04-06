using System.Security.Claims;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.UserAppointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants;
using static Hairly.GCommon.ApplicationConstants.SuccessMessages;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;

namespace Hairly.Web.Controllers
{
    [Authorize(Roles = UserRoleName)]
    public class UserAppointmentController : Controller
    {
        private readonly IUserAppointmentService userAppointmentService;

        public UserAppointmentController(IUserAppointmentService userAppointmentService)
        {
            this.userAppointmentService = userAppointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var appointments = await userAppointmentService.GetMyAppointmentsAsync(userId);

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Book()
        {
            var model = await userAppointmentService.GetBookingModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(UserAppointmentBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await userAppointmentService.GetBookingModelAsync();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var isBooked = await userAppointmentService.BookAppointmentAsync(model, userId);

            if (isBooked)
            {
                TempData[SuccessMessageKey] = UserAppointmentBookedSuccessfully;
                return RedirectToAction(nameof(MyAppointments));
            }

            ModelState.AddModelError(string.Empty, UserAppointmentBookingError);
            model = await userAppointmentService.GetBookingModelAsync();
            return View(model);
        }
    }
}
