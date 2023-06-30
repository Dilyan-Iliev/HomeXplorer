namespace HomeXplorer.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class ResetPasswordViewModel
    {
        public string UserId { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength,
            ErrorMessage = PasswordRequirements)]
        public string NewPassword { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
