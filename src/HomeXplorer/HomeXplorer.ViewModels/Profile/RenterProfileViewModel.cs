namespace HomeXplorer.ViewModels.Profile
{
    public class RenterProfileViewModel
        : BaseProfileViewModel
    {
        public RenterProfileViewModel()
        {
            this.AddedReviews = new List<ProfileReviewViewModel>();
        }

        public int TotalLikedProperties { get; set; }

        public int TotalRentedProperties { get; set; }

        public int TotalReviews { get; set; }

        public IEnumerable<ProfileReviewViewModel> AddedReviews { get; set; }
    }
}
