namespace HomeXplorer.Data.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Seeding;

    public class CountryConfiguration
        : IEntityTypeConfiguration<Country>
    {
        private readonly CountrySeeder seeder;

        public CountryConfiguration()
        {
            this.seeder = new CountrySeeder();
        }

        public void Configure(EntityTypeBuilder<Country> builder)
        {
            var countries = this.seeder.GenerateCountries();

            builder.HasData(countries);
        }
    }
}
