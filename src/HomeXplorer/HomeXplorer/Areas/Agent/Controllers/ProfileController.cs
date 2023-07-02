namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;
    using Newtonsoft.Json;
    using System.Text;

    public class ProfileController
        : BaseAgentController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            string userId = this.User.GetId();

            try
            {
                var downloadUrl = Url.Action(nameof(DownloadProfileInfo), "Profile", new { userId });

                var agent = await this.profileService.GetAgentProfileInfo(userId);
                agent.DownloadPersonalInfoUrl = downloadUrl;
                
                return this.View(agent);

            }
            catch (Exception)
            {
                this.TempData["ProfileError"] = "Something went wrong, try again";
                //TODO add this tempdata to the redirected view
                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }

        }

        [HttpGet]
        public async Task<IActionResult> DownloadProfileInfo(string userId)
        {
            var agentProfile = await profileService.GetAgentProfileInfo(userId);

            // Serialize the agent profile to JSON
            var json = JsonConvert.SerializeObject(agentProfile, Formatting.Indented);

            // Convert the JSON string to a byte array
            var data = Encoding.UTF8.GetBytes(json);

            // Return the file as a download
            return File(data, "text/plain", "agent_profile.txt");
        }
    }
}
