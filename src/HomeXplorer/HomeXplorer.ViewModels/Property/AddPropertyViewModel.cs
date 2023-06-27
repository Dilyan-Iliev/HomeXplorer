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
            this.Countries = new HashSet<SelectCountryViewModel>();
            this.Cities = new HashSet<SelectCityViewModel>();
            this.PropertyTypes = new HashSet<SelectPropertyTypeViewModel>();
            this.BuildingTypes = new HashSet<SelectBuildingTypeViewModel>();
            this.Images = new HashSet<IFormFile>();
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
        public int BuildintTypeId { get; set; }

        public IEnumerable<SelectBuildingTypeViewModel> BuildingTypes { get; set; }

        public ICollection<IFormFile> Images { get; set; }
    }
}
