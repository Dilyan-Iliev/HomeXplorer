namespace HomeXplorer.Tests.CountryTests
{
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using HomeXplorer.Services.Interfaces;

    public class CountryServiceTests
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
        public void Country_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Act
            var countryService = new CountryService(dbContext);

            //Assert
            Assert.That(countryService, Is.Not.Null);
        }

        [Test]
        public async Task GetCountriesAsync_Should_Return_Correct_Data()
        {
            //Arrange
            var countriesData = new List<Country>
            {
                new Country { Id = 1, Name = "Country 1" },
                new Country { Id = 2, Name = "Country 2" }
            };

            dbContext.Countries.AddRange(countriesData);
            dbContext.SaveChanges();

            var countryService = new CountryService(dbContext);

            // Act
            var result = await countryService.GetCountriesAsync();

            var mappedDbContextCountryNames = dbContext.Countries
                .Select(c => c.Name)
                .ToList();

            var mappedServiceCountryNames = result
                .Select(c => c.Name)
                .ToList();

            var mappedDbContextCountryIds = dbContext.Countries
                .Select(c => c.Id)
                .ToList();

            var mappedServiceCountryIds = result
                .Select(c => c.Id)
                .ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(dbContext.Countries.Count(), Is.EqualTo(result.Count()));
                Assert.That(mappedServiceCountryNames, Is.EqualTo(mappedDbContextCountryNames));
                Assert.That(mappedServiceCountryIds, Is.EqualTo(mappedDbContextCountryIds));
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
