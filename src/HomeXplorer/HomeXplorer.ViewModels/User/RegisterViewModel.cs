namespace HomeXplorer.ViewModels.User
{
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;
    using static HomeXplorer.Common.ErrorConstants;

    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            this.Countries = new List<SelectCountryViewModel>();
            this.Cities = new List<SelectCityViewModel>();
        }

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength,
            ErrorMessage = FieldLength)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength,
            ErrorMessage = FieldLength)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [RegularExpression(EmailRegex)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [RegularExpression(PhoneNumberRegex)]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        public string Role { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(PasswordMinLength, ErrorMessage = PasswordRequirements)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [Compare(nameof(Password), ErrorMessage = PasswordsMissmatch)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        public int CountryId { get; set; }

        public IEnumerable<SelectCountryViewModel> Countries { get; set; }

        public int CityId { get; set; }
        
        public IEnumerable<SelectCityViewModel> Cities { get; set; }
    }
}