namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Profile;

    public interface IProfileService
    {
        Task<AgentProfileViewModel> GetAgentProfileInfoAsync(string userId);

        Task UpdateProfilePictureAsync(string userId, string profilePictureUrl);

        //Task<IEnumerable<ProfilePropertiesViewModel>> GetAgentLastFourProperties(string userId);
    }
}
