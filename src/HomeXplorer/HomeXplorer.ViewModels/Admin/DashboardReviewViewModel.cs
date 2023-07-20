namespace HomeXplorer.ViewModels.Admin
{
    using HomeXplorer.ViewModels.Property.Renter;

    public class DashboardReviewViewModel
        : IndexReviewViewModel
    {
        public int Id { get; set; }

        public string AddedOn { get; set; } = null!;

        public bool IsApproved { get; set; }
    }
}
