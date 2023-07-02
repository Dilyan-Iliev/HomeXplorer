namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CloudImageConfiguration
        : IEntityTypeConfiguration<CloudImage>
    {
        private readonly CloudImageSeeder seeder;

        public CloudImageConfiguration()
        {
            this.seeder = new CloudImageSeeder();
        }

        public void Configure(EntityTypeBuilder<CloudImage> builder)
        {
            var images = this.seeder.GenerateCloudImages();

            builder.HasData(images);
        }
    }
}
