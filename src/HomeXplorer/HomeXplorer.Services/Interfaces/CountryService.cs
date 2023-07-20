﻿namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Country;

    public class CountryService
        : ICountryService
    {
        private readonly IRepository repo;

        public CountryService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<SelectCountryViewModel>> GetCountriesAsync()
        {
            return await this.repo
                .AllReadonly<Country>()
                .Select(c => new SelectCountryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
