namespace HomeXplorer.Tests.CityTests
{
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;

    public class CityServiceTests
    {
        private DbContextOptions<HomeXplorerDbContext> options;
        private HomeXplorerDbContext dbContext;

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<HomeXplorerDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDataBase")
                .Options;

            dbContext = new HomeXplorerDbContext(options);
        }

        [Test]
        public void City_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Arrange
            ICityService cityService = new CityService(dbContext);

            //Assert
            Assert.That(cityService, Is.Not.Null);
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

            ICityService cityService = new CityService(dbContext);

            //Act
            var result = await cityService.GetAllCitiesByCountryIdAsync(country.Id);

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

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            //dbContext.Dispose();
        }
    }
}
