using Hairly.Services.Core.Contracts;
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
    }
}
