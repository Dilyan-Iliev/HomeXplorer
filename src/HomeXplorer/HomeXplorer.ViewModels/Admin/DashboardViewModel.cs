namespace HomeXplorer.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.Reviews = new List<DashboardReviewViewModel>();
        }

        public int TotalPropertiesUploaded { get; set; }

        public int TotalLikesOfProperties { get; set; }

        public int TotalRentedProperties { get; set; }

        public int TotalReviewsAdded { get; set; }

        public IEnumerable<DashboardReviewViewModel> Reviews { get; set; }
    }
}
