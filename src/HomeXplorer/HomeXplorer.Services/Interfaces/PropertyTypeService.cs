namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyType;
    using HomeXplorer.Core.Contexts;

    public class PropertyTypeService
        : IPropertyTypeService
    {
        private readonly HomeXplorerDbContext dbContext;

        public PropertyTypeService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectPropertyTypeViewModel>> GetPropertyTypesAsync()
        {
            return await this.dbContext
                .PropertyTypes
                .AsNoTracking()
                .Select(p => new SelectPropertyTypeViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();
        }
    }
}
