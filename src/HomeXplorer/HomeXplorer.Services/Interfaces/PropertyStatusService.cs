namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyStatus;
    using HomeXplorer.Core.Contexts;

    public class PropertyStatusService
        : IPropertyStatusService
    {
        private readonly HomeXplorerDbContext dbContext;

        public PropertyStatusService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectPropertyStatusViewModel>> GetPropertyStatusesAsync()
        {
            return await this.dbContext
                .PropertyStatuses
                .AsNoTracking()
                .Select(ps => new SelectPropertyStatusViewModel()
                {
                    Id = ps.Id,
                    Name = ps.Name
                })
                .ToListAsync();
        }
    }
}
