using Hairly.Services.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hairly.Web.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? hairdresserId = GetUserId();
            var clients = await clientService.GetAllClientsAsync(hairdresserId);

            return View(clients);
        }
    }
}
