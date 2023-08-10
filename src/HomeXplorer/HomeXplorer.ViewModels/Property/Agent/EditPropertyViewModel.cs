namespace HomeXplorer.ViewModels.Property.Agent
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.ViewModels.PropertyType;
    using HomeXplorer.ViewModels.PropertyStatus;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.PropertyConstants;

    public class EditPropertyViewModel
    {
        public EditPropertyViewModel()
        {
            this.NewImages = new List<IFormFile>();
            this.Countries = new List<SelectCountryViewModel>();
            this.Cities = new List<SelectCityViewModel>();
            this.PropertyTypes = new List<SelectPropertyTypeViewModel>();
            this.BuildingTypes = new List<SelectBuildingTypeViewModel>();
            this.PropertyStatuses = new List<SelectPropertyStatusViewModel>();
            this.AddedImages = new List<PropertyImagesViewModel>();
            this.DeletedPhotosIds = new List<int>();
            this.Errors = new List<string>();
        }

        public string AgentId { get; set; } = null!;

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
            ErrorMessage = RangeError)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        [Range(MinSize, MaxSize, ErrorMessage = RangeError)]
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

        public int PropertyStatusId { get; set; }

        public IEnumerable<SelectPropertyStatusViewModel> PropertyStatuses { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        public int BuildingTypeId { get; set; }

        public IEnumerable<SelectBuildingTypeViewModel> BuildingTypes { get; set; }

        public ICollection<IFormFile>? NewImages { get; set; }

        public IEnumerable<PropertyImagesViewModel> AddedImages { get; set; }

        public ICollection<int>? DeletedPhotosIds { get; set; }

        public ICollection<string>? Errors { get; set; }
    }
}
