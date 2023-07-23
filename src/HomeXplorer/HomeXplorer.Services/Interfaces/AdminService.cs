namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Data.Models.Entities;
    using HomeXplorer.Core.Contexts;

    public class AdminService
        : IAdminService
    {
        private readonly HomeXplorerDbContext dbContext;
        private readonly ICountryService countryService;

        public AdminService(
            HomeXplorerDbContext dbContext,
            ICountryService countryService)
        {
            this.dbContext = dbContext;
            this.countryService = countryService;
        }

        public async Task<bool> AddNewBuildingTypeAsync(AddNonExistingBuildingTypeViewModel buildingType)
        {
            bool buildingTypeExist = await this.dbContext
                .BuildingTypes
                .AnyAsync(bt => bt.Name == buildingType.Name);

            if (!buildingTypeExist)
            {
                await this.dbContext.BuildingTypes.AddAsync(new BuildingType() { Name = buildingType.Name });
                await this.dbContext.SaveChangesAsync();
            }

            return buildingTypeExist;
        }

        public async Task<bool> AddNewCityAsync(AddNonExistingCityToExistingCountryViewModel city)
        {
            bool cityExist = await this.dbContext
                .Cities
                .AnyAsync(c => c.Name == city.CityName);

            if (!cityExist)
            {
                City newCity = new()
                {
                    Name = city.CityName,
                    CountryId = city.CountryId,
                };

                await this.dbContext.Cities.AddAsync(newCity);
                await this.dbContext.SaveChangesAsync();
            }

            return cityExist;
        }

        public async Task<bool> AddNewCountryAsync(AddNonExistingCountryViewModel country)
        {
            bool countryExists = await this.dbContext
                .Countries
                .AnyAsync(c => c.Name == country.Name);

            if (!countryExists)
            {
                await this.dbContext.Countries.AddAsync(new Country() { Name = country.Name });
                await this.dbContext.SaveChangesAsync();
            }

            return countryExists;
        }

        public async Task<bool> AddNewPropertyTypeAsync(AddNonExistingPropertyTypeViewModel propertyType)
        {
            bool propertyTypeExist = await this.dbContext
                .PropertyTypes
                .AnyAsync(pt => pt.Name == propertyType.Name);

            if (!propertyTypeExist)
            {
                await this.dbContext.PropertyTypes.AddAsync(new PropertyType() { Name = propertyType.Name });
                await this.dbContext.SaveChangesAsync();
            }

            return propertyTypeExist;
        }

        public async Task ApproveReviewAsync(int reviewId)
        {
            Review? review = await this.dbContext
                .Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review != null)
            {
                review.IsApproved = true;
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            Review? review = await this.dbContext
                .Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review != null)
            {
                this.dbContext.Reviews.Remove(review);
                await this.dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllAgentsViewModel>> GetAllAgentsStatisticsAsync()
        {
            return await this.dbContext
                .Agents
                .AsNoTracking()
                .Select(a => new AllAgentsViewModel()
                {
                    FullName = $"{a.User.FirstName} {a.User.LastName}",
                    City = a.City.Name,
                    Country = a.City.Country.Name,
                    ProfileImageUrl = a.ProfilePictureUrl,
                    TotalPropertiesUploaded = a.Properties.Count,
                    TotalPropertiesRented = a.Properties.Count(p => p.RenterId != null),
                    TotalPropertiesLiked = this.dbContext
                        .RentersPropertiesFavorites
                        .AsNoTracking()
                        .Count(favorite => favorite.PropertyId != null
                        && a.Properties.Any(prop => prop.Id == favorite.PropertyId))
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllBuildingTypesAsync()
        {
            return await this.dbContext
                .BuildingTypes
                .AsNoTracking()
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
            return await this.dbContext
                .Reviews
                .AsNoTracking()
                .Where(r => !r.IsApproved)
                .Select(r => new DashboardReviewViewModel()
                {
                    Id = r.Id,
                    AddedOn = r.AddedOn.ToString("MM/dd/yyyy"),
                    Description = r.Description,
                    ReviewCreatorName = $"{r.ReviewCreator.User.FirstName} {r.ReviewCreator.User.LastName}",
                    ReviewCreatorAvatarUrl = r.ReviewCreator.ProfilePictureUrl,
                    IsApproved = r.IsApproved
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllPropertyTypesAsync()
        {
            return await this.dbContext
                .PropertyTypes
                .AsNoTracking()
                .Select(pt => pt.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<AllRentersViewModel>> GetAllRentersStatisticsAsync()
        {
            return await this.dbContext
                .Renters
                .AsNoTracking()
                .Select(r => new AllRentersViewModel()
                {
                    FullName = $"{r.User.FirstName} {r.User.LastName}",
                    City = r.City.Name,
                    Country = r.City.Country.Name,
                    ProfileImageUrl = r.ProfilePictureUrl,
                    TotalPropertiesRented = r.RentedProperties!.Count,
                    TotalPropertiesLiked = r.FavouriteProperties!.Count,
                    TotalReviewsAdded = r.Reviews.Count,
                })
                .ToListAsync();
        }

        public async Task<DashboardViewModel> GetDashboardInfoAsync()
        {
            DashboardViewModel dashboardStatistics = new()
            {
                TotalPropertiesUploaded =
                await this.dbContext
                .Properties
                .AsNoTracking()
                .CountAsync(),

                TotalReviewsAdded =
                await this.dbContext
                .Reviews
                .AsNoTracking()
                .CountAsync(),

                TotalLikesOfProperties =
                await this.dbContext
                .RentersPropertiesFavorites
                .AsNoTracking()
                .CountAsync(),

                TotalRentedProperties =
                await this.dbContext
                .Properties
                .AsNoTracking()
                .CountAsync(p => p.RenterId != null),

                Reviews =
                await this.dbContext
                    .Reviews
                    .AsNoTracking()
                    .Select(r => new DashboardReviewViewModel()
                    {
                        AddedOn = r.AddedOn.ToString("MM/dd/yyyy"),
                        Description = $"{r.Description.Substring(0, 20)}...",
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