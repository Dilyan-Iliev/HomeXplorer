namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyStatus;
    using HomeXplorer.Data.Entities;
    using Microsoft.EntityFrameworkCore;

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
