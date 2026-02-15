using Hairly.Services.Core.Contracts;
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
    }
}
