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
            try
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
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClientEditViewModel?> GetClientForEditAsync(int id, string? hairdresserId)
        {
            return await dbContext.Clients
                .Where(c => c.Id == id && c.HairdresserId == hairdresserId && !c.IsDeleted)
                .Select(c => new ClientEditViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    Note = c.Note
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateClientAsync(ClientEditViewModel viewModel, string? hairdresserId)
        {
            try
            {
                var client = await dbContext.Clients
                    .FirstOrDefaultAsync(c => c.Id == viewModel.Id && c.HairdresserId == hairdresserId && !c.IsDeleted);

                if (client == null)
                {
                    return false;
                }

                client.FirstName = viewModel.FirstName;
                client.LastName = viewModel.LastName;
                client.PhoneNumber = viewModel.PhoneNumber;
                client.Email = viewModel.Email;
                client.Note = viewModel.Note;

                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClientDetailsViewModel> GetClientDetailsAsync(int id, string? hairdresserId)
        {
            return await dbContext.Clients
                .Where(c => c.Id == id && c.HairdresserId == hairdresserId && !c.IsDeleted)
                .Select(c => new ClientDetailsViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    Note = c.Note,
                    CreatedOn = c.CreatedOn,
                    RecentAppointments = c.Appointments
                        .Where(a => !a.IsDeleted)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Take(10)
                        .Select(a => new ClientAppointmentViewModel
                        {
                            Id = a.Id,
                            AppointmentDate = a.AppointmentDate,
                            ServiceName = a.Service.Name,
                            ServicePrice = a.Service.Price,
                            Status = a.Status.ToString()
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Client not found.");
        }
    }
}
