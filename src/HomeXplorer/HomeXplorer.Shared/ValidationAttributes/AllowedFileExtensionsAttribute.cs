namespace HomeXplorer.Shared.ValidationAttributes
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class AllowedFileExtensionsAttribute
        : ValidationAttribute
    {
        private readonly string[] allowedExtensions;

        public AllowedFileExtensionsAttribute(params string[] allowedExtensions)
        {
            this.allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile? file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName)?.ToLower().Substring(1);

                if (!allowedExtensions.Contains(extension))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
