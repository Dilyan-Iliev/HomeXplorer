namespace HomeXplorer.Data.Seeding
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Seeding;
    using System.Text.Json;

    public class CitySeeder
    {
        public static ICollection<City>? GenerateCities()
        {
            //check the path
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\..\bg.json");
            string jsonFile = File.ReadAllText(jsonPath);

            CityViewModel[]? cityModels = JsonSerializer.Deserialize<CityViewModel[]>(jsonFile);

            return cityModels?.Select(c => new City
            {
                Id = c.Id,
                Name = c.Name,
                CountryId = c.CountryId
            }).ToList() ?? null;

            //CityViewModel[]? cityModels =
            //    JsonSerializer.Deserialize<CityViewModel[]>(jsonFile);

            //var cities = new List<City>();

            //if (cityModels != null)
            //{
            //    foreach (var c in cityModels)
            //    {
            //        City city = new City
            //        {
            //            Id = c.Id,
            //            Name = c.Name,
            //            CountryId = c.CountryId,
            //        };

            //        cities.Add(city);
            //    }

            //    return cities;
            //}

            //return null;
        }
    }
}
