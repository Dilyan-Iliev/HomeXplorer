namespace HomeXplorer.Core.Contexts
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Entities.Configuration;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HomeXplorerDbContext : IdentityDbContext<ApplicationUser>
    {
        public HomeXplorerDbContext(DbContextOptions<HomeXplorerDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Agency> Agencies { get; set; }

        public DbSet<Agent> Agents { get; set; }

        public DbSet<Renter> Renters { get; set; }

        public DbSet<CloudImage> CloudImages { get; set; }

        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyStatus> PropertyStatuses { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<PageVisit> PageVisits { get; set; }

        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
