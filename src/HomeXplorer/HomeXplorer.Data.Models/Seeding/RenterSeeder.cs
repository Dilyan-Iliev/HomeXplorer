namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    using static HomeXplorer.Common.DataConstants.ApplicationUserConstants;

    public class RenterSeeder
    {
        public ICollection<Renter> GenerateRenters()
        {
            var renters = new List<Renter>()
            {
                new Renter()
                {
                    Id = 1,
                    CityId = 1,
                    UserId = "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                    ProfilePictureUrl = DefaultUserProfilePictureUrl
                }
            };

            return renters;
        }
    }
}
