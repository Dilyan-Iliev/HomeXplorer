namespace HomeXplorer.Services.Interfaces
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.BuildingType;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BuildingTypeService
        : IBuildingTypeService
    {
        private readonly IRepository repo;

        public BuildingTypeService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<SelectBuildingTypeViewModel>> GetBuildingTypesAsync()
        {
            return await this.repo
                .AllReadonly<BuildingType>()
                .Select(b => new SelectBuildingTypeViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();
        }
    }
}
