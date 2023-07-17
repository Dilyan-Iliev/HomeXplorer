namespace HomeXplorer.Areas.Agent.Controllers
{
    using System.Text;

    using Newtonsoft.Json;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;
    using HomeXplorer.ViewModels.Profile;

    public class ProfileController
        : BaseAgentController
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

                var agent = await this.profileService.GetAgentProfileInfoAsync(userId);
                agent.DownloadPersonalInfoUrl = downloadUrl!;

                return this.View(agent);

            }
            catch (Exception)
            {
                this.TempData["ProfileError"] = "Something went wrong, try again";
                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DownloadProfileInfo(string userId)
        {
            //so that this action to not be found by different user
            string currentUserId = this.User.GetId();

            if (currentUserId != userId)
            {
                //TODO: 
                //return view for unauthorized
            }

            var agentProfile = await profileService.GetAgentProfileInfoAsync(userId);

            // Serialize the agent profile to JSON
            var json = JsonConvert.SerializeObject(agentProfile, Formatting.Indented);

            // Convert the JSON string to a byte array
            var data = Encoding.UTF8.GetBytes(json);

            // Return the file as a download
            return File(data, "text/plain", "agent_profile.txt");
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
                await this.profileService.UpdateAgentProfilePictureAsync(userId, imageUrl);

                return this.RedirectToAction(nameof(MyProfile));
            }
            catch (Exception)
            {
                this.TempData["ProfileError"] = "Something went wrong, try again";
                return this.RedirectToAction("MyProfie", "Profile", new { area = Agent });
            }
        }
    }
}
