namespace HomeXplorer.Data.Seeding
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Seeding;
    using System.Text.Json;

    public class CitySeeder
    {
        public ICollection<City> GenerateCities()
        {
            string jsonPath = "../bg.json"; //check the path
            string jsonFile = File.ReadAllText(jsonPath);

            CityViewModel[] cityModels = JsonSerializer.Deserialize<CityViewModel[]>(jsonFile);

            var cities = new List<City>();

            foreach (var c in cityModels)
            {
                City city = new City
                {
                    Name = c.Name,
                    CountryId = c.CountryId,
                };

                cities.Add(city);
            }

            return cities;
        }
    }
}
