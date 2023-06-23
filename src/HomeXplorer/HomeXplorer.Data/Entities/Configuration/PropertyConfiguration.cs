namespace HomeXplorer.Data.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PropertyConfiguration
        : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .HasOne(x => x.Renter)
                .WithMany(a => a.RentedProperties);
        }
    }
}
