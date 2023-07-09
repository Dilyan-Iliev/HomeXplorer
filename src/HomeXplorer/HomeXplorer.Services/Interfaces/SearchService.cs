namespace HomeXplorer.Services.Interfaces
{
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Search;
    using System.Threading.Tasks;

    public class SearchService
        : ISearchService
    {
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IBuildingTypeService buildingTypeService;
        private readonly ICountryService countryService;

        public SearchService(IPropertyTypeService propertyTypeService,
            IBuildingTypeService buildingTypeService,
            ICountryService countryService)
        {
            this.propertyTypeService = propertyTypeService;
            this.buildingTypeService = buildingTypeService;
            this.countryService = countryService;
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
    }
}
