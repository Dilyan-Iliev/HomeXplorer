namespace HomeXplorer.ViewModels.Admin
{
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;

    public class AllCountriesWithCitiesViewModel
    {
        public AllCountriesWithCitiesViewModel()
        {
            this.Countries = new List<SelectCountryViewModel>();
            this.Cities = new List<SelectCityViewModel>();
        }

        public int CountryId { get; set; }

        public IEnumerable<SelectCountryViewModel> Countries { get; set; }

        public int CityId { get; set; }

        public IEnumerable<SelectCityViewModel> Cities { get; set; }
    }
}
