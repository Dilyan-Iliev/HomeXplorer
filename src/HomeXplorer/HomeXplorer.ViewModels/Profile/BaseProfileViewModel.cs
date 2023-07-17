namespace HomeXplorer.ViewModels.Profile
{
    public class BaseProfileViewModel
    {
        //public BaseProfileViewModel()
        //{
        //    this.PropertyImages = new List<ProfilePropertiesViewModel>();
        //}

        public string FullName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string? PersonalImage { get; set; }

        public string DownloadPersonalInfoUrl { get; set; } = null!;

        //public IEnumerable<ProfilePropertiesViewModel> PropertyImages { get; set; }
    }
}
