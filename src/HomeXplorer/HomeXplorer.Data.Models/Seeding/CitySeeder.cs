namespace HomeXplorer.Data.Seeding
{
    using System.Text.Json;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Seeding;

    public class CitySeeder
    {
        public static ICollection<City>? GenerateCities()
        {
            var cities = new List<City>();

            var jsonPaths = new List<string>
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\..\bg.json"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\..\gr.json")
            };

            foreach (var jsonPath in jsonPaths)
            {
                string jsonFile = File.ReadAllText(jsonPath);

                CityViewModel[]? cityModels = JsonSerializer.Deserialize<CityViewModel[]>(jsonFile);

                if (cityModels != null)
                {
                    cities.AddRange(cityModels.Select(c => new City
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CountryId = c.CountryId
                    }));
                }
            }

            return cities.Any() ? cities : null;
        }
    }
}
