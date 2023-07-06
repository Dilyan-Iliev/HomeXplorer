namespace HomeXplorer.ViewModels.Property.Renter
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            this.SliderProperties = new List<IndexSliderPropertyViewModel>();
            this.LastThreePropertiesNearby = new List<LatestPropertiesViewModel>();
            this.LatestProperties = new List<LatestPropertiesViewModel>();
        }

        public IEnumerable<IndexSliderPropertyViewModel> SliderProperties { get; set; }

        public IEnumerable<LatestPropertiesViewModel>? LastThreePropertiesNearby { get; set; }

        public IEnumerable<LatestPropertiesViewModel> LatestProperties { get; set; }
    }
}
