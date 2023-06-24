namespace HomeXplorer.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class LoginViewModel
    {
        [Required(ErrorMessage = FieldRequired)]
        [RegularExpression(EmailRegex)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = FieldRequired)]
        public string Password { get; set; } = null!;
    }
}
