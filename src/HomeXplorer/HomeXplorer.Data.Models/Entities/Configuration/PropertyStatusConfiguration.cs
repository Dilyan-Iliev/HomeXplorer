namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;

    public class PropertyStatusConfiguration
        : IEntityTypeConfiguration<PropertyStatus>
    {
        private readonly PropertyStatusSeeder seeder;

        public PropertyStatusConfiguration()
        {
            this.seeder = new PropertyStatusSeeder();
        }

        public void Configure(EntityTypeBuilder<PropertyStatus> builder)
        {
            var propertyStatus = this.seeder.GeneratePropertyStatuses();

            builder.HasData(propertyStatus);
        }
    }
}
