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
    }
}
