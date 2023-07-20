namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;

    public class PropertyTypeConfiguration
        : IEntityTypeConfiguration<PropertyType>
    {
        private readonly PropertyTypeSeeder seeder;

        public PropertyTypeConfiguration()
        {
            this.seeder = new PropertyTypeSeeder();
        }

        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            var propertyTypes = this.seeder.GeneratePropertyTypes();

            builder.HasData(propertyTypes);
        }
    }
}
