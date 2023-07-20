namespace HomeXplorer.Services.Interfaces
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

        public async Task<bool> AddNewBuildingTypeAsync(AddNonExistingBuildingTypeViewModel buildingType)
        {
            bool buildingTypeExist = await this.repo
                .All<BuildingType>()
                .AnyAsync(bt => bt.Name == buildingType.Name);

            if (!buildingTypeExist)
            {
                await this.repo.AddAsync<BuildingType>(new BuildingType() { Name = buildingType.Name });
                await this.repo.SaveChangesAsync();
            }

            return buildingTypeExist;
        }

        public async Task<bool> AddNewCityAsync(AddNonExistingCityToExistingCountryViewModel city)
        {
            bool cityExist = await this.repo
                .All<City>()
                .AnyAsync(c => c.Name == city.CityName);

            if (!cityExist)
            {
                City newCity = new()
                {
                    Name = city.CityName,
                    CountryId = city.CountryId,
                };

                await this.repo.AddAsync<City>(newCity);
                await this.repo.SaveChangesAsync();
            }

            return cityExist;
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

        public async Task ApproveReviewAsync(int reviewId)
        {
            Review? review = await this.repo
                .GetByIdAsync<Review>(reviewId);

            if (review != null)
            {
                review.IsApproved = true;
                await this.repo.SaveChangesAsync();
            }
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            Review? review = await this.repo
                .GetByIdAsync<Review>(reviewId);

            if (review != null)
            {
                await this.repo.DeleteAsync<Review>(reviewId);
                await this.repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetAllBuildingTypesAsync()
        {
            return await this.repo
                .AllReadonly<BuildingType>()
                .Select(bt => bt.Name)
                .ToListAsync();
        }

        public async Task<AllCountriesWithCitiesViewModel> GetAllCitiesFromCountryAsync()
        {
            return new AllCountriesWithCitiesViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync()
            };
        }

        public async Task<AllCountriesWithCitiesViewModel> GetAllCountriesAsync()
        {
            return new AllCountriesWithCitiesViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync()
            };
        }

        public async Task<IEnumerable<DashboardReviewViewModel>> GetAllPendingReviewsAsync()
        {
            return await this.repo
                .All<Review>()
                .Where(r => !r.IsApproved)
                .Select(r => new DashboardReviewViewModel()
                {
                    Id = r.Id,
                    AddedOn = r.AddedOn.ToString("MM/dd/yyyy"),
                    Description = $"{r.Description.Substring(0, 10)}...",
                    ReviewCreatorName = $"{r.ReviewCreator.User.FirstName} {r.ReviewCreator.User.LastName}",
                    ReviewCreatorAvatarUrl = r.ReviewCreator.ProfilePictureUrl,
                    IsApproved = r.IsApproved
                })
                .ToListAsync();
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
                        IsApproved = r.IsApproved
                    })
                    .ToListAsync()
            };

            return dashboardStatistics;
        }
    }
}
