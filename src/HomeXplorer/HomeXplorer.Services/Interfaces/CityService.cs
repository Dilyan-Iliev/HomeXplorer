namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Core.Contexts;

    public class CityService
        : ICityService
    {
        private readonly HomeXplorerDbContext dbContext;

        public CityService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCityViewModel>> GetAllCitiesByCountryIdAsync(int countryId)
        {
            return await this.dbContext
                .Cities
                .AsNoTracking()
                .Where(c => c.CountryId == countryId)
                .Select(c => new SelectCityViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
