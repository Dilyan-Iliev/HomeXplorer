namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
