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
        private readonly HomeXplorerDbContext dbContext;

        public CountryService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<SelectCountryViewModel>> GetCountriesAsync()
        {
            return await this.dbContext
                .Countries
                .AsNoTracking()
                .Select(c => new SelectCountryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
