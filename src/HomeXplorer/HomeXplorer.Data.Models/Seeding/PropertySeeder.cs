namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class PropertySeeder
    {
        public ICollection<Property> GenerateProperties()
        {
            var properties = new List<Property>()
            {
                new Property()
                {
                    Id = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799"),
                    Name = "Tranquil Haven Villa",
                    Description = "Welcome to Tranquil Solace Villa, a serene escape nestled amidst lush landscapes. This charming villa offers a perfect blend of luxury and comfort. Immerse yourself in the peaceful ambiance, away from the hustle and bustle, and experience the joy of simple living surrounded by nature's beauty",
                    Address = "123 Serenity Lane, Greenhaven, Sofia, 12345",
                    Price = 1250,
                    Size = 100,
                    CityId = 1,
                    PropertyTypeId = 1,
                    BuildingTypeId = 1,
                    PropertyStatusId = 1,
                    AgentId = 1,
                    RenterId = null,
                    AddedOn = DateTime.UtcNow
                },

                new Property()
                {
                    Id = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26"),
                    Name = "Serenity Woods Retreat",
                    Description = "Discover Serenity Woods Retreat, a picturesque hideaway set in a woodland paradise. This enchanting retreat offers a cozy sanctuary where you can unwind and rejuvenate. Embrace the soothing sounds of nature, and let the stress melt away in this delightful haven of tranquility.",
                    Address = "456 Tranquility Road, Woodland Springs, Plovdiv, 67890",
                    Price = 1000,
                    Size = 180,
                    CityId = 2,
                    PropertyTypeId = 4,
                    BuildingTypeId = 2,
                    PropertyStatusId = 1,
                    AgentId = 1,
                    RenterId = null,
                    AddedOn = DateTime.UtcNow
                },

                new Property()
                {
                    Id = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453"),
                    Name = "Enchanted Meadow Chalet",
                    Description = "Step into the enchanting world of Enchanted Meadow Chalet, where fairy tales come to life. This whimsical chalet is surrounded by lush meadows, creating a magical ambiance that promises a unique and memorable experience. Indulge in the charm of this extraordinary abode and create cherished memories in this one-of-a-kind retreat.",
                    Address = "789 Enchantment Avenue, Meadowland Heights, Sofia, 54321",
                    Price = 850,
                    Size = 95,
                    CityId = 1,
                    PropertyTypeId = 1,
                    BuildingTypeId = 1,
                    PropertyStatusId = 1,
                    AgentId = 1,
                    RenterId = null,
                    AddedOn = DateTime.UtcNow
                },

                new Property()
                {
                    Id = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07"),
                    Name = "Harmony Heights Villa",
                    Description = "Welcome to Harmony Heights Villa, an exclusive hilltop residence offering breathtaking panoramic views. This luxurious villa combines elegance with the serenity of its elevated location. Enjoy a life of opulence and privacy in this stunning retreat, where you can revel in the beauty of the surroundings while indulging in modern comforts.",
                    Address = "987 Harmony Hill, Summitview, Sofia, 24680",
                    Price = 1400,
                    Size = 150,
                    CityId = 1,
                    PropertyTypeId = 1,
                    BuildingTypeId = 1,
                    PropertyStatusId = 1,
                    AgentId = 1,
                    RenterId = null,
                    AddedOn = DateTime.UtcNow
                }
            };

            return properties;
        }
    }
}
