namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;

    public class RenterConfiguration
        : IEntityTypeConfiguration<Renter>
    {
        private readonly RenterSeeder seeder;

        public RenterConfiguration()
        {
            this.seeder = new RenterSeeder();
        }

        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder
                .HasMany(x => x.FavouriteProperties)
                .WithOne(x => x.Renter)
                .HasForeignKey(x => x.RenterId)
                .OnDelete(DeleteBehavior.NoAction);

            var renters = seeder.GenerateRenters();
            builder.HasData(renters);
        }
    }
}
