namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class PropertyTypeSeeder
    {
        public ICollection<PropertyType> GeneratePropertyTypes()
        {
            return new List<PropertyType>
            {
                new PropertyType()
                {
                    Id = 1,
                    Name = "Villa"
                },

                new PropertyType()
                {
                    Id = 2,
                    Name = "Single-Family Home"
                },

                new PropertyType()
                {
                    Id = 3,
                    Name = "Townhome"
                },

                new PropertyType()
                {
                    Id = 4,
                    Name = "Bungalow"
                },

                new PropertyType()
                {
                    Id = 5,
                    Name = "Ranch"
                },

                new PropertyType()
                {
                    Id = 6,
                    Name = "Studio"
                },

                new PropertyType()
                {
                    Id = 7,
                    Name = "Residential area"
                }
            };
        }
    }
}
