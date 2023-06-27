namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;

    public class PropertyStatusSeeder
    {
        public ICollection<PropertyStatus> GeneratePropertyStatuses()
        {
            var propertyStatuses = new List<PropertyStatus>()
            {
                new PropertyStatus()
                {
                    Id = 1,
                    Name = "Free"
                },

                new PropertyStatus()
                {
                    Id = 2,
                    Name = "Reserved"
                },

                new PropertyStatus()
                {
                    Id = 3,
                    Name = "Taken"
                }
            };

            return propertyStatuses;
        }
    }
}
