﻿namespace HomeXplorer.Data.Seeding
{
    using HomeXplorer.Data.Entities;

    public class CountrySeeder
    {
        public ICollection<Country> GenerateCountries()
        {
            var countries = new List<Country>
            {
                new Country()
                {
                    Id = 1,
                    Name = "Bulgaria"
                }
            };

            return countries;
        }
    }
}


