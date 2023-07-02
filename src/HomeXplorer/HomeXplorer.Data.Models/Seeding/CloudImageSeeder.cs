namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class CloudImageSeeder
    {
        public ICollection<CloudImage> GenerateCloudImages()
        {
            return new List<CloudImage>
            {
                new CloudImage
                {
                    Id = 1,
                    Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1688283726/default-avatar-profile-icon-of-social-media-user-vector_lcoi8s.jpg",
                }
            };
        }
    }
}
