namespace HomeXplorer.ViewModels.Search
{
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.ViewModels.PropertyType;

    public class PropertySearchViewModel
    {
        public PropertySearchViewModel()
        {
            this.PropertyTypes = new List<SelectPropertyTypeViewModel>();
            this.BuildingTypes = new List<SelectBuildingTypeViewModel>();
            this.Countries = new List<SelectCountryViewModel>();
            this.Cities = new List<SelectCityViewModel>();
        }

        public string? SearchTerm { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectPropertyTypeViewModel> PropertyTypes { get; set; }

        public int BuildingTypeId { get; set; }

        public IEnumerable<SelectBuildingTypeViewModel> BuildingTypes { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<SelectCountryViewModel> Countries { get; set; }

        public int CityId { get; set; }

        public IEnumerable<SelectCityViewModel> Cities { get; set; }
    }
}
