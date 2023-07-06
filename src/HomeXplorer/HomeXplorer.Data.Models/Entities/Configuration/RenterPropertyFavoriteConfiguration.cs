namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RenterPropertyFavoriteConfiguration
        : IEntityTypeConfiguration<RenterPropertyFavorite>
    {
        public void Configure(EntityTypeBuilder<RenterPropertyFavorite> builder)
        {
            builder
                .HasKey(x => new
                {
                    x.PropertyId,
                    x.RenterId
                });
        }
    }
}
