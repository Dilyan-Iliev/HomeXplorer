namespace HomeXplorer.ViewModels.Property.Renter
{
    using HomeXplorer.ViewModels.Property.Agent;

    public class RenterDetailsPropertyViewModel
        : DetailsPropertyViewModel
    {
        public string AgentProfilePicture { get; set; } = null!;

        public string AgentFullName { get; set; } = null!;

        public string AgentEmail { get; set; } = null!;

        public string AgentPhone { get; set; } = null!;
    }
}
