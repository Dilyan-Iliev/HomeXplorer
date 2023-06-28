namespace HomeXplorer.Services.Interfaces
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.PropertyType;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
