namespace HomeXplorer.ViewModels.Property
{
    using HomeXplorer.ViewModels.Property.Renter;

    public class BaseMainPageViewModel
    {
        public BaseMainPageViewModel()
        {
            this.SliderProperties = new List<IndexSliderPropertyViewModel>();
            this.LatestProperties = new List<LatestPropertiesViewModel>();
            this.ApprovedReviews = new List<IndexReviewViewModel>();
        }

        public IEnumerable<IndexSliderPropertyViewModel> SliderProperties { get; set; }

        public IEnumerable<LatestPropertiesViewModel> LatestProperties { get; set; }

        public IEnumerable<IndexReviewViewModel> ApprovedReviews { get; set; }
    }
}
