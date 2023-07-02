namespace HomeXplorer.ViewModels.Profile
{
    using HomeXplorer.ViewModels.Property.Agent;

    public class AgentProfileViewModel
    {
        public AgentProfileViewModel()
        {
            this.PropertyImages = new List<PropertyImagesViewModel>();
        }

        public string FullName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? PersonalImage { get; set; } //add some default picture when they dont have one

        public int TotalUploadedProperties { get; set; }

        public int TotalRentedProperties { get; set; }

        public string DownloadPersonalInfoUrl { get; set; }

        public IEnumerable<PropertyImagesViewModel> PropertyImages { get; set; }
    }
}
