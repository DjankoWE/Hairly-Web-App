using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Service;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Services.Core
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext dbContext;

        public ServiceService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceIndexViewModel>> GetAllServicesAsync(string hairdresserId)
        {
            return await dbContext.Services
                .Where(s => s.HairdresserId == hairdresserId && !s.IsDeleted)
                .Select(s => new ServiceIndexViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price,
                    DurationInMinutes = s.DurationInMinutes
                })
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<bool> CreateServiceAsync(ServiceCreateViewModel viewModel, string hairdresserId)
        {
            try
            {
                var service = new Service
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Price = viewModel.Price,
                    DurationInMinutes = viewModel.DurationInMinutes,
                    HairdresserId = hairdresserId,
                    IsDeleted = false
                };

                await dbContext.Services.AddAsync(service);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
