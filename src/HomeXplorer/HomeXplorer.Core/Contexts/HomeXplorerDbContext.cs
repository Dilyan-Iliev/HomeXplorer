﻿namespace HomeXplorer.Core.Contexts
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());

            base.OnModelCreating(builder);
        }
    }
}