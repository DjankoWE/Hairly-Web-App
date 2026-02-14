using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Client;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Services.Core
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext dbContext;

        public ClientService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ClientIndexViewModel>> GetAllClientsAsync(string? hairdresserId)
        {
            return await dbContext.Clients
                .Where(c => c.HairdresserId == hairdresserId && !c.IsDeleted)
                .Select(c => new ClientIndexViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    TotalAppointments = c.Appointments.Count(a => !a.IsDeleted)
                })
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ThenByDescending(c => c.TotalAppointments)
                .ToListAsync();

        }

        public async Task<bool> CreateClientAsync(ClientCreateViewModel viewModel, string? hairdresserId)
        {
            var client = new Client
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                Note = viewModel.Note,
                HairdresserId = hairdresserId!,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };

            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
