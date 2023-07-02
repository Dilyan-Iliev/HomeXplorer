namespace HomeXplorer.ViewModels.Profile
{
    using Microsoft.AspNetCore.Http;

    public class UpdateProfilePictureViewModel
    {
        public IFormFile ProfilePicture { get; set; } = null!;
    }
}
