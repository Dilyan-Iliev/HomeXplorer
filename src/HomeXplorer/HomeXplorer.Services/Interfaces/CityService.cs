namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using HomeXplorer.Core.Contexts;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CityService
        : ICityService
    {
        private readonly HomeXplorerDbContext context;

        public CityService(HomeXplorerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectCityViewModel>> GetAllCitiesByCountryIdAsync(int countryId)
        {
            return await this.context
                .Cities
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
