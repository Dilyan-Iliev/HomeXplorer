namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BuildingTypeConfiguration
        : IEntityTypeConfiguration<BuildingType>
    {
        private readonly BuildingTypeSeeder seeder;

        public BuildingTypeConfiguration()
        {
            this.seeder = new BuildingTypeSeeder();
        }

        public void Configure(EntityTypeBuilder<BuildingType> builder)
        {
            var buildingTypes = this.seeder.GenerateBuildingTypes();

            builder.HasData(buildingTypes);
        }
    }
}
