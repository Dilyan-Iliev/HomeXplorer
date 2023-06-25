namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using HomeXplorer.ViewModels.City;
    using HomeXplorer.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;

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
