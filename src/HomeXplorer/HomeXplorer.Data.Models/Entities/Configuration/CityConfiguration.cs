namespace HomeXplorer.Data.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Seeding;

    public class CityConfiguration
        : IEntityTypeConfiguration<City>
    {
        private readonly CitySeeder seeder;

        public CityConfiguration()
        {
            this.seeder = new CitySeeder();
        }

        public void Configure(EntityTypeBuilder<City> builder)
        {
            var cities = CitySeeder.GenerateCities();

            if (cities != null)
            {
                builder.HasData(cities);
            }
        }
    }
}
