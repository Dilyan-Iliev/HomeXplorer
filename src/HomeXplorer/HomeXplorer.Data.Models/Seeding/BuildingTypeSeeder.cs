namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class BuildingTypeSeeder
    {
        public ICollection<BuildingType> GenerateBuildingTypes()
        {
            return new List<BuildingType>()
            {
                new BuildingType()
                {
                    Id = 1,
                    Name = "Luxury"
                },

                new BuildingType()
                {
                    Id = 2,
                    Name = "Average"
                },

                new BuildingType()
                {
                    Id = 3,
                    Name = "Ordinary"
                }
            };
        }
    }
}
