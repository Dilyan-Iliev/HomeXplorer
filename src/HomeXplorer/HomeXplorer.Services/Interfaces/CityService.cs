namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;

    public class CityService
        : ICityService
    {
        private readonly IRepository repo;

        public CityService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<SelectCityViewModel>> GetAllCitiesByCountryIdAsync(int countryId)
        {
            return await this.repo
                .AllReadonly<City>()
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
