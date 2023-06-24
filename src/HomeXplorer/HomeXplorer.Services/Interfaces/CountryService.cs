namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Country;

    public class CountryService
        : ICountryService
    {
        private readonly HomeXplorerDbContext context;

        public CountryService(HomeXplorerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<SelectCountryViewModel>> GetCountriesAsync()
        {
            return await this.context
                .Countries
                .Select(c => new SelectCountryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
