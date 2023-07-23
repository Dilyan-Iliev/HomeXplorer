namespace HomeXplorer.Tests.AdminTests
{
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.Services.Interfaces;

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

            await dbContext.BuildingTypes.AddRangeAsync(buildingTypes);
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

            await dbContext.BuildingTypes.AddRangeAsync(buildingTypes);
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

            await dbContext.BuildingTypes.AddRangeAsync(buildingTypes);
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

            await dbContext.Countries.AddRangeAsync(countries);
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

            await dbContext.Cities.AddRangeAsync(cities);
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

            await dbContext.Cities.AddRangeAsync(cities);
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

        [Test]
        public async Task Add_New_Property_Type_Method_Should_Return_True_If_Property_Type_Alread_Exists()
        {
            //Arrange
            // Prepare existing property types data
            var propertyTypes = new[]
            {
                new PropertyType() { Name = "Test"},
                new PropertyType() { Name = "Villa"},
            };
            await dbContext.AddRangeAsync(propertyTypes);
            await dbContext.SaveChangesAsync();

            var propertyTypeModel = new AddNonExistingPropertyTypeViewModel()
            {
                Name = "Villa"
            };

            var countryServiceMock = new Mock<ICountryService>();
            var repo = new Repository(dbContext);

            var adminService = new AdminService(repo, countryServiceMock.Object);

            //Act
            // Call the method under test to attempt adding a new property type
            bool result = await adminService.AddNewPropertyTypeAsync(propertyTypeModel);

            //Assert
            // The method should return true since the property type "Villa" already exists
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_Property_Type_Method_Should_Return_False_If_Property_Type_Does_Not_Exist()
        {
            //Arrange
            // Create the model for a new property type with a name that does not exist in the database
            var propertyTypeModel = new AddNonExistingPropertyTypeViewModel()
            {
                Name = "Villa"
            };

            var countryServiceMock = new Mock<ICountryService>();
            var repo = new Repository(dbContext);

            var adminService = new AdminService(repo, countryServiceMock.Object);

            //Act
            // Call the method under test to attempt adding a new property type
            bool result = await adminService.AddNewPropertyTypeAsync(propertyTypeModel);

            //Assert
            // The method should return false since the property type "Villa" does not exist
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Add_New_Property_Type_Method_Should_Successfully_Add_New_Property_Type()
        {
            //Arrange
            // Create the model for a new property type with a unique name
            var propertyTypeModel = new AddNonExistingPropertyTypeViewModel()
            {
                Name = "Villa"
            };

            var countryServiceMock = new Mock<ICountryService>();
            var repo = new Repository(dbContext);

            var adminService = new AdminService(repo, countryServiceMock.Object);

            //Act
            // Call the method under test to add a new property type
            await adminService.AddNewPropertyTypeAsync(propertyTypeModel);

            //Assert
            // Verify that the new property type is successfully added to the database
            Assert.That(await dbContext.PropertyTypes
                .AnyAsync(x => x.Name == propertyTypeModel.Name),
                    Is.True);
        }

        [Test]
        public async Task Approve_Review_Method_Should_Change_Review_ApproveStatus_To_True_If_Review_Is_Found()
        {
            //Arrange
            // Create a new review with a unique ID
            Review review = new()
            {
                Id = 1,
                Description = "Test",
            };

            await dbContext.Reviews.AddAsync(review);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Act
            // Call the method under test with the review's ID
            await adminService.ApproveReviewAsync(review.Id);

            //Assert
            // Verify that the review's IsApproved property is now true
            Assert.That(review.IsApproved, Is.True);
        }

        [Test]
        public async Task Approve_Review_Method_Should_Not_Change_Review_ApproveStatus_If_Review_Is_Not_Found()
        {
            //Arrange
            // Create a review with an ID that does not exist in the database
            Review review = new()
            {
                Id = 123,
                Description = "Test"
            };

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Act
            // Call the method under test with the non-existent review's ID
            await adminService.ApproveReviewAsync(review.Id);

            //Assert
            // Verify that the review's IsApproved property remains false
            Assert.That(review.IsApproved, Is.False);
        }

        [Test]
        public async Task Delete_Review_Method_Should_Remove_Review_If_Review_Is_Found()
        {
            //Arrange
            // Create two review objects with distinct IDs
            Review firstReview = new() { Id = 1, Description = "Test1" };
            Review secondReview = new() { Id = 2, Description = "Test2" };

            await dbContext.Reviews.AddAsync(firstReview);
            await dbContext.Reviews.AddAsync(secondReview);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Act
            // Call the method under test with the ID of the second review
            await adminService.DeleteReviewAsync(secondReview.Id);

            //Assert
            // Use Assert.Multiple to perform multiple assertions in a single test
            Assert.Multiple(async () =>
            {
                Assert.That(await dbContext.Reviews.CountAsync(), Is.EqualTo(1));
                Assert.That(await dbContext.Reviews
                    .AnyAsync(r => r.Id == secondReview.Id),
                    Is.False);
            });
        }

        [Test]
        public async Task Delete_Review_Method_Should_Not_Remove_Review_If_Review_Is_Not_Found()
        {
            //Arrange
            // Create a review object with an ID that is not present in the database
            Review review = new()
            {
                Id = 321,
                Description = "Test"
            };

            var mockedCountryService = new Mock<ICountryService>();

            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Act
            // Call the method under test with the ID of the review (321), which is not in the database
            await adminService.DeleteReviewAsync(review.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                // Verify that the review with ID 321 is not found in the database
                Assert.That(await dbContext.Reviews.AnyAsync(r => r.Id == review.Id), Is.False);

                // Verify that the total number of reviews in the database remains the same
                Assert.That(await dbContext.Reviews.CountAsync(), Is.EqualTo(0));
            });
        }

        //[Test]
        //public async Task Get_All_Cities_From_Country_Should_Return_Correct_Data()
        //{
        //    var countryModel = new Country()
        //    {
        //        Id = 1,
        //        Name = "Bulgaria"
        //    };
        //    await dbContext.Countries.AddAsync(countryModel);

        //    var cities = new List<City>()
        //    {
        //        new City() {Name = "Sofia", CountryId = countryModel.Id},
        //        new City() {Name = "Plovdiv", CountryId = countryModel.Id},
        //        new City() {Name = "Varna", CountryId = countryModel.Id},
        //    };
        //    await dbContext.Cities.AddRangeAsync(cities);
        //    await dbContext.SaveChangesAsync();

        //    var mockedCountryService = new Mock<ICountryService>();
        //    mockedCountryService.Setup(s => s.GetCountriesAsync()).ReturnsAsync(new List<SelectCountryViewModel>
        //    {
        //        new SelectCountryViewModel { Id = 1, Name = "Bulgaria" }
        //        // Add more countries if needed
        //    });

        //    var repository = new Repository(dbContext);
        //    var adminService = new AdminService(repository, mockedCountryService.Object);

        //    // Act
        //    var result = await adminService.GetAllCitiesFromCountryAsync();

        //    Assert.That(result, Is.Not.Null);

        //}

        [Test]
        public async Task Get_All_Property_Types_Method_Should_Return_Correct_Data()
        {
            //Arrange
            var propertyTypes = new List<PropertyType>()
            {
                new PropertyType() {Name = "Test1"},
                new PropertyType() {Name = "Test2"},
            };

            await dbContext.PropertyTypes.AddRangeAsync(propertyTypes);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();
            var repo = new Repository(dbContext);

            var adminService = new AdminService(repo, mockedCountryService.Object);

            //Act
            var result = await adminService.GetAllPropertyTypesAsync();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.GreaterThan(0));
                Assert.That(result,
                    Is.EqualTo(propertyTypes.Select(pt => pt.Name).ToList()));
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