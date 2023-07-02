namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Profile;

    public interface IProfileService
    {
        Task<AgentProfileViewModel> GetAgentProfileInfo(string userId);
    }
}
