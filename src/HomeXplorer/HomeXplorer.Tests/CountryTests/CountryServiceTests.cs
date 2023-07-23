namespace HomeXplorer.Tests.CountryTests
{
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.Services.Contracts;

    public class CountryServiceTests
        : BaseTestsOptions
    {
        private ICountryService countryService;

        [SetUp]
        public void SetUp()
        {
            countryService = new CountryService(dbContext);
        }

        [Test]
        public void Country_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
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
    }
}
