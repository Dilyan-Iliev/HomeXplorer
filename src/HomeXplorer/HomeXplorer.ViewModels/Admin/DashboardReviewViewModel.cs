namespace HomeXplorer.ViewModels.Admin
{
    using HomeXplorer.ViewModels.Property.Renter;

    public class DashboardReviewViewModel
        : IndexReviewViewModel
    {
        public string AddedOn { get; set; } = null!;

        public bool IsApproved { get; set; }
    }
}
