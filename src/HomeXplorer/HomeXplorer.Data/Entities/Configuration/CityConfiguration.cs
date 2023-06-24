namespace HomeXplorer.Data.Entities.Configuration
{
    using HomeXplorer.Data.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
