namespace HomeXplorer.ViewModels.Property
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.ViewModels.PropertyType;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.PropertyConstants;

    public class AddPropertyViewModel
    {
        public AddPropertyViewModel()
        {
            this.Countries = new List<SelectCountryViewModel>();
            this.Cities = new List<SelectCityViewModel>();
            this.PropertyTypes = new List<SelectPropertyTypeViewModel>();
            this.BuildingTypes = new List<SelectBuildingTypeViewModel>();
            this.Images = new List<IFormFile>();
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = FieldLength)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinlength,
            ErrorMessage = FieldLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public int Size { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength,
            ErrorMessage = FieldLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        public int CountryId { get; set; }

        public IEnumerable<SelectCountryViewModel> Countries { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public int CityId { get; set; }

        public IEnumerable<SelectCityViewModel> Cities { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectPropertyTypeViewModel> PropertyTypes { get; set; }

        public int PropertyStatusId { get; set; } = 1;

        [Required(ErrorMessage = FieldRequired)]
        public int BuildingTypeId { get; set; }

        public IEnumerable<SelectBuildingTypeViewModel> BuildingTypes { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public ICollection<IFormFile> Images { get; set; }
    }
}
