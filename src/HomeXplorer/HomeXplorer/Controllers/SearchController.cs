namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Search;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Enums;

    using static HomeXplorer.Common.TempDataConstants;

    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;
        private readonly IAgentPropertyService propertyService;

        public SearchController(
            ISearchService searchService,
            IAgentPropertyService propertyService)
        {
            this.searchService = searchService;
            this.propertyService = propertyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Result([FromQuery] PropertySearchViewModel searchResultModel,
            int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            try
            {
                bool selectedCountryIdExists = await this.ValidateDropdownAsync<Country>(searchResultModel.CityId);
                bool selectedCityIdExists = await this.ValidateDropdownAsync<City>(searchResultModel.CityId);
                bool selectedPropertyTypeIdExists = await this.ValidateDropdownAsync<PropertyType>(searchResultModel.PropertyTypeId);
                bool selectedBuildingTypeIdExists = await this.ValidateDropdownAsync<BuildingType>(searchResultModel.BuildingTypeId);

                if (!selectedCountryIdExists || !selectedCityIdExists
                        || !selectedPropertyTypeIdExists || !selectedBuildingTypeIdExists)
                {
                    this.TempData["InvalidDropdownOption"] = "You must choose a valid option from the dropdowns";

                    return this.View();
                }

                var resultModel =
                    await this.searchService.SearchResult(searchResultModel, pageNumber, pageSize, propertySorting);
                return this.View(resultModel);
            }
            catch (Exception)
            {
                this.TempData["SearchError"] = "Something went wrong, please try again";
                return this.View();
            }
        }

        private async Task<bool> ValidateDropdownAsync<T>(int dropdownId) where T : class
        {
            if (dropdownId != 0)
            {
                bool isValid = await this.propertyService.ExistByIdAsync<T>(dropdownId);
                return isValid;
            }

            return true; //if dropdown is 0 i won't check it at all -
                         //this means the user didn't search by this parameter
        }
    }
}
