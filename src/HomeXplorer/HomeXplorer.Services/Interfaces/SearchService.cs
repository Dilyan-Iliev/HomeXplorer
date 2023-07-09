namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

    using HomeXplorer.ViewModels.Search;
    using HomeXplorer.Services.Contracts;

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
