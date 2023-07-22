namespace HomeXplorer.Tests.AdminTests
{
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.ViewModels.Country;

    public class AdminServiceTests
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
        public void Admin_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Arrange
            // Create mocks for the dependencies (ICountryService and IRepository)
            var mockedCountryService = new Mock<ICountryService>();
            var mockedRepo = new Mock<IRepository>();

            //Act
            // Create an instance of the AdminService with the mocked dependencies
            var adminService = new AdminService(mockedRepo.Object, mockedCountryService.Object);

            //Assert
            // Verify that the AdminService instance is not null, indicating successful initialization
            Assert.That(adminService, Is.Not.Null);
        }

        [Test]
        public async Task Get_All_Building_Types_Method_Should_Return_Correct_Data()
        {
            //Arrange
            // Create a list of BuildingType entities to be added to the in-memory database
            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Luxury"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Ordinary"},
            };

            dbContext.BuildingTypes.AddRange(buildingTypes);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            // Act
            // Call the method under test to retrieve all building types
            var result = await adminService.GetAllBuildingTypesAsync();

            // Assert
            // Verify that the result is not null and matches the expected building type names
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.EqualTo(null));
                Assert.That(result,
                    Is.EqualTo(buildingTypes.Select(bt => bt.Name)));
            });
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Return_True_If_DB_Already_Has_The_Building_Type()
        {
            //Arrange
            // Create a list of BuildingType entities to be added to the in-memory database
            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Test"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Test2"},
            };

            dbContext.BuildingTypes.AddRange(buildingTypes);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var buildingTypeModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Average"
            };

            //Act
            // Call the method under test to add a new building type that already exists
            var result = await adminService.AddNewBuildingTypeAsync(buildingTypeModel);

            //Assert
            // Verify that the method returns true, indicating the building type already exists in the database
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Return_False_If_DB_Does_Not_Have_The_Building_Type()
        {
            //Arrange
            // Create a list of BuildingType entities to be added to the in-memory database
            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Test"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Test2"},
            };

            dbContext.BuildingTypes.AddRange(buildingTypes);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            // Use the real repository with the in-memory database context
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var buildingTypeViewModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Ordinary"
            };

            //Act
            // Call the method under test to add a new building type that does not exist
            var result = await adminService.AddNewBuildingTypeAsync(buildingTypeViewModel);

            //Assert
            // Verify that the method returns false, indicating the building type does not exist in the database
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Successfully_Add_New_Building_Type()
        {
            //Arrange
            // Create a mock for ICountryService
            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var buildingTypeViewModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Ordinary"
            };

            //Act
            // Call the method under test to add a new building type
            await adminService.AddNewBuildingTypeAsync(buildingTypeViewModel);

            //Assert
            // Verify that the new building type is successfully added to the database
            Assert.That(await dbContext.BuildingTypes
                .AnyAsync(x => x.Name == buildingTypeViewModel.Name),
                    Is.True);
        }

        [Test]
        public async Task Add_New_Country_Method_Should_Return_True_If_DB_Alread_Has_The_Country()
        {
            //Arrange
            // Create an array of Country entities to be added to the in-memory database
            var countries = new[]
            {
                new Country() { Name = "Test"},
                new Country() { Name = "Sofia"}
            };

            dbContext.Countries.AddRange(countries);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var countryModel = new AddNonExistingCountryViewModel()
            {
                Name = "Sofia"
            };

            //Act
            // Call the method under test to add a new country that already exists
            var result = await adminService.AddNewCountryAsync(countryModel);

            //Assert
            // Verify that the method returns true, indicating the country already exists in the database
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_Country_Method_Should_Return_False_If_DB_Does_Not_Have_The_Country()
        {
            //Arange
            // Create a mock for ICountryService
            var mockedCountryService = new Mock<ICountryService>();
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var countryModel = new AddNonExistingCountryViewModel()
            {
                Name = "Test"
            };

            //Act
            // Call the method under test to add a new country that does not exist
            var result = await adminService.AddNewCountryAsync(countryModel);

            //Assert
            // Verify that the method returns false, indicating the country does not exist in the database
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Add_New_Country_Method_Should_Successfully_Add_New_Country()
        {
            //Arrange
            // Create a mock for ICountryService
            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var countryModel = new AddNonExistingCountryViewModel()
            {
                Name = "Plovdiv"
            };

            //Act
            // Call the method under test to add a new country
            await adminService.AddNewCountryAsync(countryModel);

            //Assert
            // Verify that the new country is successfully added to the database
            Assert.That(await dbContext.Countries
                .AnyAsync(x => x.Name == countryModel.Name),
                    Is.True);
        }

        [Test]
        public async Task Add_New_City_Method_Should_Return_True_If_City_Already_Exists_In_The_Country()
        {
            //Arrange
            // Prepare the country model and existing cities data
            var countryModel = new SelectCountryViewModel()
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var cities = new List<City>
            {
                new City() {Name = "Sofia", CountryId = countryModel.Id},
                new City() {Name = "Test", CountryId = countryModel.Id},
                new City() {Name = "Plovdiv", CountryId = countryModel.Id},
            };

            dbContext.Cities.AddRange(cities);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var cityModel = new AddNonExistingCityToExistingCountryViewModel()
            {
                CityName = "Sofia",
                CountryId = countryModel.Id
            };

            //Act
            // Attempt to add an existing city (Sofia) to the existing country (Bulgaria)   
            bool result = await adminService.AddNewCityAsync(cityModel);

            //Assert
            // The method should return true as the city already exists in the country.
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_City_Method_Should_Return_False_If_City_Does_Not_Exist_In_The_Country()
        {
            //Arrange
            // Prepare the country model and existing cities data
            var countryModel = new SelectCountryViewModel()
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var cities = new List<City>
            {
                new City() {Name = "Sofia", CountryId = countryModel.Id},
                new City() {Name = "Test", CountryId = countryModel.Id},
                new City() {Name = "Plovdiv", CountryId = countryModel.Id},
            };

            dbContext.Cities.AddRange(cities);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var cityModel = new AddNonExistingCityToExistingCountryViewModel()
            {
                CityName = "Burgas",
                CountryId = countryModel.Id
            };

            //Act
            // Attempt to add a new city "Burgas" to the existing country (Bulgaria)
            bool result = await adminService.AddNewCityAsync(cityModel);

            //Assert
            // The method should return false as the city "Burgas" does not exist in the country (Bulgaria).
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Add_New_City_Method_Should_Successfully_Add_New_City()
        {
            //Arrange
            //Prepare mocks
            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var countryModel = new SelectCountryViewModel()
            {
                Id = 1,
                Name = "Bulgaria"
            };

            var cityModel = new AddNonExistingCityToExistingCountryViewModel()
            {
                CityName = "Varna",
                CountryId = countryModel.Id
            };

            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Act
            // Call the method under test to add a new city
            await adminService.AddNewCityAsync(cityModel);

            //Assert
            // Verify that the new city is successfully added to the database
            Assert.That(await dbContext.Cities
                .AnyAsync(x => x.Name == cityModel.CityName),
                    Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            //dbContext.Dispose();
        }
    }
}