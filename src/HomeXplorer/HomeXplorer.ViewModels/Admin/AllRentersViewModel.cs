namespace HomeXplorer.ViewModels.Admin
{
    using static HomeXplorer.Common.UserRoleConstants;

    public class AllRentersViewModel
        : BaseAllUsersViewModel
    {
        public string Role { get; set; } = Renter;

        public int TotalReviewsAdded { get; set; }
    }
}
