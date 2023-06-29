namespace HomeXplorer.ViewModels.Property.Agent
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.ViewModels.PropertyType;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.PropertyConstants;
    using HomeXplorer.Shared.ValidationAttributes;

    public class AddPropertyViewModel
    {
        public AddPropertyViewModel()
        {
            Countries = new List<SelectCountryViewModel>();
            Cities = new List<SelectCityViewModel>();
            PropertyTypes = new List<SelectPropertyTypeViewModel>();
            BuildingTypes = new List<SelectBuildingTypeViewModel>();
            Images = new List<IFormFile>();
            Errors = new List<string>();
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
        [Range(typeof(decimal), "250", "100000",
            ErrorMessage = FieldLength)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        [Range(MinSize, MaxSize, ErrorMessage = FieldLength)]
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

        [Required(ErrorMessage = FieldRequired)]
        public IEnumerable<SelectCityViewModel> Cities { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public int PropertyTypeId { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public IEnumerable<SelectPropertyTypeViewModel> PropertyTypes { get; set; }

        public int PropertyStatusId { get; set; } = 1;

        [Required(ErrorMessage = FieldRequired)]
        public int BuildingTypeId { get; set; }

        public IEnumerable<SelectBuildingTypeViewModel> BuildingTypes { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        //[AllowedFileExtensions("jpg", "png", "jpeg", ErrorMessage = "Not allowed file extension - only jpg, png and jpeg")]
        public ICollection<IFormFile> Images { get; set; }

        public ICollection<string>? Errors { get; set; }
    }
}
