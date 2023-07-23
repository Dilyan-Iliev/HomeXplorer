namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.Core.Contexts;

    public class BuildingTypeService
        : IBuildingTypeService
    {
        private readonly HomeXplorerDbContext dbContext;

        public BuildingTypeService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectBuildingTypeViewModel>> GetBuildingTypesAsync()
        {
            return await this.dbContext
                .BuildingTypes
                .AsNoTracking()
                .Select(b => new SelectBuildingTypeViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToListAsync();
        }
    }
}
