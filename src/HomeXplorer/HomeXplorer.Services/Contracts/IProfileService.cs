namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Profile;

    public interface IProfileService
    {
        Task<AgentProfileViewModel> GetAgentProfileInfoAsync(string userId);

        Task<RenterProfileViewModel> GetRenterProfileInfoAsync(string userId);

        Task UpdateAgentProfilePictureAsync(string userId, string profilePictureUrl);

        Task UpdateRenterProfilePictureAsync(string userId, string profilePictureUrl);
    }
}
