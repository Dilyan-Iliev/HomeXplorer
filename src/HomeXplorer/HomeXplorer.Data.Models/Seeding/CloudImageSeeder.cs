namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class CloudImageSeeder
    {
        public ICollection<CloudImage> GenerateImages()
        {
            return new List<CloudImage>()
            {
                new CloudImage()
                        {
                            Id = 1,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-1_rk3g0b_umsjgr.webp",
                            PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                        },
                        new CloudImage()
                        {
                            Id = 2,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-14_eabwwr_cmcayy.webp",
                            PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                        },
                        new CloudImage()
                        {
                            Id = 3,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-24_upzvcz_wakzke.webp",
                            PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                        },
                        new CloudImage()
                        {
                            Id = 4,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554425/386038_64_St_W_Okotoks-13_xmndng_wajp67.webp",
                            PropertyId = Guid.Parse("5edf4581-d8ae-4a8f-b2f3-2c87b7d10799")
                        },
                        new CloudImage()
                        {
                            Id = 5,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-01_pyq5dc_jg4sqh.webp",
                            PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                        },
                        new CloudImage()
                        {
                            Id = 6,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-02_czzwv0_mofzcq.webp",
                            PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                        },
                        new CloudImage()
                        {
                            Id = 7,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554623/krtajna-bali-indonesia-10_mqq0yu_lyrena.webp",
                            PropertyId = Guid.Parse("62373c07-f1e7-4813-ba49-bc8a61ad8f26")
                        },
                        new CloudImage()
                        {
                            Id = 8,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/3_-_DJI_20230704202223_0050_D_ycntes_sj7twt.webp",
                            PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                        },
                        new CloudImage()
                        {
                            Id = 9,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/11_-_DJI_20230704192355_0019_D_j1gzep_gozyn7.webp",
                            PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                        },
                        new CloudImage()
                        {
                            Id = 10,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554832/1_-_DJI_20230704192923_0029_D_pqslwu_jn21nr.webp",
                            PropertyId = Guid.Parse("a9742cc5-14cf-424c-b5a5-f4ecba4e1453")
                        },
                        new CloudImage()
                        {
                            Id = 11,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain01_ftjwx5_q2uwxc.webp",
                            PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                        },
                        new CloudImage()
                        {
                            Id = 12,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain04_cqwfiu_c1piur.webp",
                            PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                        },
                        new CloudImage()
                        {
                            Id = 13,
                            Url = "https://res.cloudinary.com/degtesnvc/image/upload/v1690554994/huerta-grande-la-zubia-granada-spain12_zk2jwn_o6rh3p.webp",
                            PropertyId = Guid.Parse("e22089fd-8c9e-4600-94b7-ad946b779f07")
                        }
            };
        }
    }
}
