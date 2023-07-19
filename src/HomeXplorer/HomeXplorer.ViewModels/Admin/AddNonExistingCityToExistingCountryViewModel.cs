namespace HomeXplorer.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    using HomeXplorer.ViewModels.Country;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.CityConstants;

    public class AddNonExistingCityToExistingCountryViewModel
    {
        public AddNonExistingCityToExistingCountryViewModel()
        {
            this.Countries = new List<SelectCountryViewModel>();
        }

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = FieldLength)]
        public string CityName { get; set; } = null!;

        public int CountryId { get; set; }

        public IEnumerable<SelectCountryViewModel> Countries { get; set; }
    }
}
