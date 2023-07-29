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
                    //Images = new List<CloudImage>()
                    //{
                    //    new CloudImage()
                    //    {
                    //        Id = 1,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-1_rk3g0b_umsjgr.webp",
                    //        PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 2,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-14_eabwwr_cmcayy.webp",
                    //        PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 3,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-24_upzvcz_wakzke.webp",
                    //        PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 4,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-13_xmndng_wajp67.webp",
                    //        PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                    //    },
                    //}
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
                    //Images = new List<CloudImage>()
                    //{
                    //    new CloudImage()
                    //    {
                    //        Id = 5,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-01_pyq5dc_jg4sqh.webp",
                    //        PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 6,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-02_czzwv0_mofzcq.webp",
                    //        PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 7,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-10_mqq0yu_lyrena.webp",
                    //        PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                    //    },
                    //},
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
                    //Images = new List<CloudImage>()
                    //{
                    //    new CloudImage()
                    //    {
                    //        Id = 8,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/3_-_DJI_20230704202223_0050_D_ycntes_sj7twt.webp",
                    //        PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 9,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/11_-_DJI_20230704192355_0019_D_j1gzep_gozyn7.webp",
                    //        PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 10,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/1_-_DJI_20230704192923_0029_D_pqslwu_jn21nr.webp",
                    //        PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                    //    },
                    //}
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
                    //Images = new List<CloudImage>()
                    //{
                    //    new CloudImage()
                    //    {
                    //        Id = 11,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain01_ftjwx5_q2uwxc.webp",
                    //        PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 12,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain04_cqwfiu_c1piur.webp",
                    //        PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                    //    },
                    //    new CloudImage()
                    //    {
                    //        Id = 13,
                    //        Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain12_zk2jwn_o6rh3p.webp",
                    //        PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                    //    },
                    //}
                }
            };

            return properties;
        }
    }
}
