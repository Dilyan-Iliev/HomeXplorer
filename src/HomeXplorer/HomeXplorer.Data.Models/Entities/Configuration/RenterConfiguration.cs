namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RenterConfiguration
        : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder
                .HasMany(x => x.FavouriteProperties)
                .WithOne(x => x.Renter)
                .HasForeignKey(x => x.RenterId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
