namespace HomeXplorer.Areas.Renter.Controllers
{
    using System.Text;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
    using CloudinaryDotNet;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Profile;

    using static HomeXplorer.Common.UserRoleConstants;
    using HomeXplorer.Services.Exceptions;

    public class ProfileController : BaseRenterController
    {
        private readonly IProfileService profileService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly Cloudinary cloudinary;

        public ProfileController(IProfileService profileService,
            ICloudinaryService cloudinaryService,
            Cloudinary cloudinary)
        {
            this.profileService = profileService;
            this.cloudinaryService = cloudinaryService;
            this.cloudinary = cloudinary;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            string userId = this.User.GetId();

            try
            {
                var downloadUrl = Url.Action(nameof(DownloadProfileInfo), "Profile", new { userId });

                var renter = await this.profileService.GetRenterProfileInfoAsync(userId);
                renter.DownloadPersonalInfoUrl = downloadUrl!;

                return this.View(renter);

            }
            catch (Exception)
            {
                this.TempData["ProfileError"] = "Something went wrong, try again";
                return this.RedirectToAction("Index", "Home", new { area = Renter });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DownloadProfileInfo(string userId)
        {
            //so that this action to not be found by different user
            string currentUserId = this.User.GetId();

            if (currentUserId != userId)
            {
                this.Unauthorized();
            }

            var renterProfile = await profileService.GetRenterProfileInfoAsync(userId);

            // Serialize the agent profile to JSON
            var json = JsonConvert.SerializeObject(renterProfile, Formatting.Indented);

            // Convert the JSON string to a byte array
            var data = Encoding.UTF8.GetBytes(json);

            // Return the file as a download
            return File(data, "text/plain", "renter_profile.txt");
        }

        [HttpGet]
        public IActionResult UpdateProfilePicture()
        {
            return this.View(new UpdateProfilePictureViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfilePicture(IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                this.TempData["ErrorUpdateProfilPicture"] = "You need to select a picture";
                return this.RedirectToAction(nameof(MyProfile));
            }

            try
            {
                string userId = this.User.GetId();
                var imageUrl = await this.cloudinaryService.UploadSingle(this.cloudinary, profilePicture);
                await this.profileService.UpdateRenterProfilePictureAsync(userId, imageUrl);

                return this.RedirectToAction(nameof(MyProfile));
            }
            catch(InvalidFileExtensionException)
            {
                this.TempData["InvalidFile"] = "Allowed file types are: jpg, png, jpeg!";
                return this.RedirectToAction(nameof(MyProfile), "Profile", new { area = Renter });
            }
            catch (Exception)
            {
                this.TempData["ProfileError"] = "Something went wrong, try again";
                return this.RedirectToAction(nameof(MyProfile), "Profile", new { area = Renter });
            }
        }
    }
}
