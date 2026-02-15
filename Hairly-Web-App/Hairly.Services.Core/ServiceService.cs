using Hairly.Data;
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
    }
}
