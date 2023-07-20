namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyType;

    public class PropertyTypeService
        : IPropertyTypeService
    {
        private readonly IRepository repo;

        public PropertyTypeService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<SelectPropertyTypeViewModel>> GetPropertyTypesAsync()
        {
            return await this.repo
                .AllReadonly<PropertyType>()
                .Select(p => new SelectPropertyTypeViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();
        }
    }
}
