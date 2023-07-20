namespace HomeXplorer.ViewModels.Admin
{
    public class BaseAllUsersViewModel
    {
        public string FullName { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string ProfileImageUrl { get; set; } = null!;

        public int TotalPropertiesRented { get; set; }

        public int TotalPropertiesLiked { get; set; }
    }
}
