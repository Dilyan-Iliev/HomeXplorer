namespace HomeXplorer.Data.Entities.Configuration
{
    using HomeXplorer.Data.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
