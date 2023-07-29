namespace HomeXplorer.Data.Entities.Configuration
{
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PropertyConfiguration
        : IEntityTypeConfiguration<Property>
    {
        private readonly PropertySeeder seeder;

        public PropertyConfiguration()
        {
            this.seeder = new PropertySeeder();
        }

        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .HasOne(x => x.Renter)
                .WithMany(a => a.RentedProperties)
                .HasForeignKey(a => a.RenterId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.City)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(x => x.AddedToFavourites)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            var properties = seeder.GenerateProperties();
            builder.HasData(properties);
        }
    }
}