namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.ViewModels.Search;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Renter;

    public class SearchService
        : ISearchService
    {
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IBuildingTypeService buildingTypeService;
        private readonly ICountryService countryService;
        private readonly HomeXplorerDbContext dbContext;

        public SearchService(IPropertyTypeService propertyTypeService,
            IBuildingTypeService buildingTypeService,
            ICountryService countryService,
            HomeXplorerDbContext dbContext)
        {
            this.propertyTypeService = propertyTypeService;
            this.buildingTypeService = buildingTypeService;
            this.countryService = countryService;
            this.dbContext = dbContext;
        }

        public async Task<PropertySearchViewModel> FillPropertySearchbarAsync()
        {
            return new PropertySearchViewModel()
            {
                BuildingTypes = await this.buildingTypeService.GetBuildingTypesAsync(),
                PropertyTypes = await this.propertyTypeService.GetPropertyTypesAsync(),
                Countries = await this.countryService.GetCountriesAsync()
            };
        }

        public async Task<SearchResultViewModel> SearchResult(PropertySearchViewModel model,
            int pageNumber, int pageSize, PropertySorting propertySorting)
        {
            IQueryable<Property> propertiesQuery = this.dbContext
                .Properties
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                string wildCard = $"%{model.SearchTerm.ToLower()}%";

                propertiesQuery = propertiesQuery
                    .Where(p => EF.Functions.Like(p.Name, wildCard) ||
                    EF.Functions.Like(p.Description, wildCard) ||
                    EF.Functions.Like(p.Address, wildCard));
            }

            if (model.CountryId != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.City.CountryId == model.CountryId);
            }

            if (model.CityId != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.CityId == model.CityId);
            }

            if (model.BuildingTypeId != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.BuildingTypeId == model.BuildingTypeId);
            }

            if (model.PropertyTypeId != 0)
            {
                propertiesQuery = propertiesQuery
                    .Where(p => p.PropertyTypeId == model.PropertyTypeId);
            }

            propertiesQuery = propertiesQuery
                .Where(p => p.Price >= model.MinPrice);

            propertiesQuery = propertiesQuery
                .Where(p => p.Price <= model.MaxPrice);

            propertiesQuery = propertySorting switch
            {
                PropertySorting.Cheapest => propertiesQuery.OrderBy(p => p.Price),
                PropertySorting.MostExpensive => propertiesQuery.OrderByDescending(p => p.Price),
                PropertySorting.Oldest => propertiesQuery.OrderBy(p => p.AddedOn),
                PropertySorting.Newest => propertiesQuery.OrderByDescending(p => p.AddedOn),
                _ => propertiesQuery
            };

            int totalProperties = await propertiesQuery.CountAsync();
            int totalPages = (int)Math.Ceiling(totalProperties / (double)pageSize);
            var currentPage = pageNumber;

            if (totalPages > 0 && currentPage > totalPages)
            {
                currentPage = totalPages; // Adjust the current page if it exceeds the total pages
            }

            var propertiesForPage = propertiesQuery
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            var propertiesModel = await propertiesForPage
                .Select(p => new LatestPropertiesViewModel()
                {
                    Id = p.Id,
                    City = p.City.Name,
                    Name = p.Name,
                    Price = p.Price,
                    Size = p.Size,
                    Status = p.PropertyStatus.Name,
                    AddedOn = p.AddedOn.ToString("MM/dd/yyyy"),
                    CoverImageUrl = p.Images
                        .Where(i => i.PropertyId == p.Id)
                        .Select(i => i.Url)
                        .FirstOrDefault()!,
                    //Visits
                })
                .ToListAsync();

            var returnedModel = new SearchResultViewModel()
            {
                Properties = propertiesModel,
                PropertySorting = propertySorting,
                PageNumber = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return returnedModel;
        }
    }
}
