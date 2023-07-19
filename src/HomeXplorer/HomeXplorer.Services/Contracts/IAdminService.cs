namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Admin;

    public interface IAdminService
    {
        Task<DashboardViewModel> GetDashboardInfoAsync();

        Task<bool> AddNewCountryAsync(AddNonExistingCountryViewModel country);

        Task<AllCountriesWithCitiesViewModel> GetAllCountriesAsync();
    }
}
