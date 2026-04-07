using Hairly.Services.Core.Contracts;
using Hairly.Web.Helpers;
using Hairly.Web.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;
using static Hairly.GCommon.ApplicationConstants.SuccessMessages;

namespace Hairly.Web.Controllers
{
    public class ClientController : BaseController
    {
        private const int PageSize = 10;
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search = null, int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            string hairdresserId = GetUserId();
            var clients = await clientService.GetAllClientsAsync(hairdresserId);

            if (!string.IsNullOrEmpty(search))
            {
                clients = clients.Where(c => c.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                        || c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                        || c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                        || (c.Email != null && c.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                                        );
            }

            var paginatedClients = PaginatedList<ClientIndexViewModel>
                    .Create(clients.ToList(), pageNumber, PageSize);

            ViewData["CurrentSearch"] = search;

            return View(paginatedClients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string hairdresserId = GetUserId();

            bool isCreated = await clientService.CreateClientAsync(viewModel, hairdresserId);

            if (isCreated)
            {
                TempData[SuccessMessageKey] = ClientCreatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ClientCreateError);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string hairdresserId = GetUserId();
            ClientEditViewModel? viewModel = await clientService.GetClientForEditAsync(id, hairdresserId);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            string hairdresserId = GetUserId();
            bool isUpdated = await clientService.UpdateClientAsync(viewModel, hairdresserId);

            if (isUpdated)
            {
                TempData[SuccessMessageKey] = ClientUpdatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ClientUpdateError);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string hairdresserId = GetUserId();

            ClientDetailsViewModel? viewModel = await clientService.GetClientDetailsAsync(id, hairdresserId);

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
            ClientDeleteViewModel? viewModel = await clientService.GetClientForDeleteAsync(id, hairdresserId);

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
            bool isDeleted = await clientService.DeleteClientAsync(id, hairdresserId);

            if (isDeleted)
            {
                TempData[SuccessMessageKey] = ClientDeletedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            TempData[ErrorMessageKey] = ClientDeleteError;
            return RedirectToAction(nameof(Index));
        }
    }
}
