namespace HomeXplorer.ViewModels.Property.Renter
{
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.ReviewConstants;

    public class AddReviewViewModel
    {
        public string? FullName { get; set; }

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinlength,
            ErrorMessage = FieldLength)]
        public string Description { get; set; } = null!;
    }
}
