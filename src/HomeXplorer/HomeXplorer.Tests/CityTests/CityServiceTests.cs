namespace HomeXplorer.Tests.CityTests
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;

    public class CityServiceTests
        : BaseTestsOptions
    {
        private ICityService cs;

        [SetUp]
        public void Setup()
        {
            cs = new CityService(dbContext);
        }

        [Test]
        public void City_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Assert
            Assert.That(cs, Is.Not.Null);
            Assert.That(cs, Is.TypeOf<CityService>());
            Assert.That(cs, Is.InstanceOf<ICityService>());
        }

        [Test]
        public async Task Get_All_Cities_By_Country_Id_Method_Should_Return_Correct_Data()
        {
            //Arrange
            Country country = new() { Id = 1, Name = "Bulgaria" };
            var cities = new List<City>()
            {
                new City() {Id = 1, Name = "Test1", CountryId = 1},
                new City() {Id = 2, Name = "Test2", CountryId = 1},
            };

            await dbContext.Countries.AddAsync(country);
            await dbContext.Cities.AddRangeAsync(cities);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await cs.GetAllCitiesByCountryIdAsync(country.Id);

            var mappedDbContextCityNames = dbContext.Cities
                .Select(c => c.Name)
                .ToList();

            var mappedServiceCityNames = result
                .Select(c => c.Name)
                .ToList();

            var mappedDbContextCityIds = dbContext.Cities
                .Select(c => c.Id)
                .ToList();

            var mappedServiceCityIds = result
                .Select(c => c.Id)
                .ToList();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.Count, Is.EqualTo(cities.Count));
                Assert.That(mappedServiceCityNames, Is.EqualTo(mappedDbContextCityNames));
                Assert.That(mappedServiceCityNames, Is.EqualTo(mappedDbContextCityNames));
            });
        }
    }
}
