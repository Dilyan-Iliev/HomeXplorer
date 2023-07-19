﻿namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Data.Models.Entities;

    public class AdminService
        : IAdminService
    {
        private readonly IRepository repo;
        private readonly ICountryService countryService;

        public AdminService(
            IRepository repo,
            ICountryService countryService)
        {
            this.repo = repo;
            this.countryService = countryService;
        }


        public async Task<bool> AddNewCountryAsync(AddNonExistingCountryViewModel country)
        {
            bool countryExists = await this.repo
                .All<Country>()
                .AnyAsync(c => c.Name == country.Name);

            if (!countryExists)
            {
                await this.repo.AddAsync<Country>(new Country() { Name = country.Name });
                await this.repo.SaveChangesAsync();
            }

            return countryExists;
        }

        public async Task<bool> AddNewPropertyTypeAsync(AddNonExistingPropertyTypeViewModel propertyType)
        {
            bool propertyTypeExist = await this.repo
                .All<PropertyType>()
                .AnyAsync(pt => pt.Name == propertyType.Name);

            if (!propertyTypeExist)
            {
                await this.repo.AddAsync<PropertyType>(new PropertyType() { Name = propertyType.Name });
                await this.repo.SaveChangesAsync();
            }

            return propertyTypeExist;
        }

        public async Task<AllCountriesWithCitiesViewModel> GetAllCountriesAsync()
        {
            return new AllCountriesWithCitiesViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync()
            };
        }

        public async Task<IEnumerable<string>> GetAllPropertyTypesAsync()
        {
            return await this.repo
                .AllReadonly<PropertyType>()
                .Select(pt => pt.Name)
                .ToListAsync();
        }

        public async Task<DashboardViewModel> GetDashboardInfoAsync()
        {
            DashboardViewModel dashboardStatistics = new()
            {
                TotalPropertiesUploaded =
                await this.repo.AllReadonly<Property>().CountAsync(),

                TotalReviewsAdded =
                await this.repo.AllReadonly<Review>().CountAsync(),

                TotalLikesOfProperties =
                await this.repo.AllReadonly<RenterPropertyFavorite>().CountAsync(),

                TotalRentedProperties =
                await this.repo.AllReadonly<Property>().CountAsync(p => p.RenterId != null),

                Reviews =
                await this.repo.AllReadonly<Review>()
                    .Select(r => new DashboardReviewViewModel()
                    {
                        AddedOn = r.AddedOn.ToString("MM/dd/yyyy"),
                        Description = $"{r.Description.Substring(0, 10)}...",
                        ReviewCreatorName = $"{r.ReviewCreator.User.FirstName} {r.ReviewCreator.User.LastName}",
                        ReviewCreatorAvatarUrl = r.ReviewCreator.ProfilePictureUrl,
                        //IsApproved
                    })
                    .ToListAsync()
            };

            return dashboardStatistics;
        }
    }
}
