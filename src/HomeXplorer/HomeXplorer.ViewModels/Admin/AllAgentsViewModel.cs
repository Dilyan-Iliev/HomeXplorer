namespace HomeXplorer.ViewModels.Admin
{
    using static HomeXplorer.Common.UserRoleConstants;

    public class AllAgentsViewModel
        : BaseAllUsersViewModel
    {
        public string Role { get; set; } = Agent;

        public int TotalPropertiesUploaded { get; set; }
    }
}
