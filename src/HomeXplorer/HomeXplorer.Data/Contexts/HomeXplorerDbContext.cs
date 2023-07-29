namespace HomeXplorer.Core.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Entities;
    using HomeXplorer.Data.Entities.Configuration;
    using HomeXplorer.Data.Models.Entities.Configuration;

    public class HomeXplorerDbContext : IdentityDbContext<ApplicationUser>
    {
        public HomeXplorerDbContext(DbContextOptions<HomeXplorerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; } = null!;

        public DbSet<Renter> Renters { get; set; } = null!;

        public DbSet<CloudImage> CloudImages { get; set; } = null!;

        public DbSet<BuildingType> BuildingTypes { get; set; } = null!;

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Property> Properties { get; set; } = null!;

        public DbSet<PropertyStatus> PropertyStatuses { get; set; } = null!;

        public DbSet<PropertyType> PropertyTypes { get; set; } = null!;

        public DbSet<PageVisit> PageVisits { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<RenterPropertyFavorite> RentersPropertiesFavorites { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());
            builder.ApplyConfiguration(new AgentConfiguration());
            builder.ApplyConfiguration(new RenterConfiguration());
            builder.ApplyConfiguration(new PropertyTypeConfiguration());
            builder.ApplyConfiguration(new PropertyStatusConfiguration());
            builder.ApplyConfiguration(new BuildingTypeConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new CloudImageConfiguration());
            builder.ApplyConfiguration(new RenterPropertyFavoriteConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
