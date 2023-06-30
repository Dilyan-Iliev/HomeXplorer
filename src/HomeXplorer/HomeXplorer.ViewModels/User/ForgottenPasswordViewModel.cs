namespace HomeXplorer.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;

    using static HomeXplorer.Common.ErrorConstants;
    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class ForgottenPasswordViewModel
    {
        [Required(ErrorMessage = FieldRequired)]
        [RegularExpression(EmailRegex, ErrorMessage = EmailError)]
        public string Email { get; set; } = null!;

        public string? ErrorMessage { get; set; }
    }
}
