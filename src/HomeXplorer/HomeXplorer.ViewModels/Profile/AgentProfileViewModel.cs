namespace HomeXplorer.ViewModels.Profile
{
    public class AgentProfileViewModel
    {
        public string FullName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? PersonalImage { get; set; } = null!; //add default picture when they dont have one

        public int TotalUploadedProperties { get; set; }

        
    }
}
