namespace HomeXplorer.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.BuildingTypeConstants;

    public class AddNonExistingBuildingTypeViewModel
    {
        [Required(ErrorMessage = FieldRequired)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = FieldLength)]
        public string Name { get; set; } = null!;
    }
}
