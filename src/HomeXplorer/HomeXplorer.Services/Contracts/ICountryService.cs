namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Country;

    public interface ICountryService
    {
        Task<IEnumerable<SelectCountryViewModel>> GetCountriesAsync();
    }
}
