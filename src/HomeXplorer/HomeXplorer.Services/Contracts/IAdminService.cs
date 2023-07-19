namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Admin;

    public interface IAdminService
    {
        Task<DashboardViewModel> GetDashboardInfoAsync();

        Task<bool> AddNewCountryAsync(AddNonExistingCountryViewModel country);

        Task<AllCountriesWithCitiesViewModel> GetAllCountriesAsync();

        Task<bool> AddNewPropertyTypeAsync(AddNonExistingPropertyTypeViewModel propertyType);

        Task<IEnumerable<string>> GetAllPropertyTypesAsync();

        Task<bool> AddNewBuildingTypeAsync(AddNonExistingBuildingTypeViewModel buildingType);

        Task<IEnumerable<string>> GetAllBuildingTypesAsync();

        Task<bool> AddNewCityAsync(AddNonExistingCityToExistingCountryViewModel city);

        Task<AllCountriesWithCitiesViewModel> GetAllCitiesFromCountryAsync();
    }
}
