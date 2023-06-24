namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.City;

    public interface ICityService
    {
        Task<IEnumerable<SelectCityViewModel>> GetAllCitiesByCountryIdAsync(int countryId);
    }
}
