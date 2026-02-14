using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Client;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string? hairdresserId = GetUserId();

            bool isCreated = await clientService.CreateClientAsync(viewModel, hairdresserId);

            if (isCreated)
            {
                TempData["SuccessMessage"] = "Client created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occured while creating the client!");
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string? hairdresserId = GetUserId();
            ClientEditViewModel? viewModel = await clientService.GetClientForEditAsync(id, hairdresserId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClientEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string? hairdresserId = GetUserId();
            bool isUpdated = await clientService.UpdateClientAsync(viewModel, hairdresserId);

            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Client updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occured while updating the client!");
            return View(viewModel);
        }
    }
}
