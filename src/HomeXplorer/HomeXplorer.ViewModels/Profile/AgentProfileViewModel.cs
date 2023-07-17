namespace HomeXplorer.ViewModels.Profile
{
    public class AgentProfileViewModel
        : BaseProfileViewModel
    {
        public AgentProfileViewModel()
        {
            this.PropertyImages = new List<ProfilePropertiesViewModel>();
        }

        //public string FullName { get; set; } = null!;

        //public string City { get; set; } = null!;

        //public string Country { get; set; } = null!;

        //public string Email { get; set; } = null!;

        //public string PhoneNumber { get; set; } = null!;

        //public string? PersonalImage { get; set; } //add some default picture when they dont have one

        public int TotalUploadedProperties { get; set; }

        public int TotalRentedProperties { get; set; }

        //public string DownloadPersonalInfoUrl { get; set; } = null!;

        public IEnumerable<ProfilePropertiesViewModel> PropertyImages { get; set; }
    }
}
