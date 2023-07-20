namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyStatus;

    public class PropertyStatusService
        : IPropertyStatusService
    {
        private readonly IRepository repo;

        public PropertyStatusService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<SelectPropertyStatusViewModel>> GetPropertyStatusesAsync()
        {
            return await this.repo
                .AllReadonly<PropertyStatus>()
                .Select(ps => new SelectPropertyStatusViewModel()
                {
                    Id = ps.Id,
                    Name = ps.Name
                })
                .ToListAsync();
        }
    }
}
