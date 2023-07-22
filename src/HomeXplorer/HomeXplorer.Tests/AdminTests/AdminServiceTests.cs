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
            var mockedCountryService = new Mock<ICountryService>();
            var mockedRepo = new Mock<IRepository>();

            //Act
            var adminService = new AdminService(mockedRepo.Object, mockedCountryService.Object);

            //Assert
            Assert.That(adminService, Is.Not.Null);
        }

        [Test]
        public async Task Get_All_Building_Types_Method_Should_Return_Correct_Data()
        {
            //Arrange
            SetUp();

            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Luxury"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Ordinary"},
            };

            // Seed the in-memory database with test data
            dbContext.BuildingTypes.AddRange(buildingTypes);
            await dbContext.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            // Use the real repository with the in-memory database context
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            // Act
            var result = await adminService.GetAllBuildingTypesAsync();

            // Assert
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
            SetUp();

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

            //Act
            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Assert
            var buildingTypeModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Average"
            };

            var result = await adminService.AddNewBuildingTypeAsync(buildingTypeModel);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Return_False_If_DB_Does_Not_Have_The_Building_Type()
        {
            //Arrange
            SetUp();

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
            var result = await adminService.AddNewBuildingTypeAsync(buildingTypeViewModel);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Successfully_Add_New_Building_Type()
        {
            //Arrange
            SetUp();

            var mockedCountryService = new Mock<ICountryService>();

            // Use the real repository with the in-memory database context
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var buildingTypeViewModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Ordinary"
            };

            //Act
            await adminService.AddNewBuildingTypeAsync(buildingTypeViewModel);

            //Assert
            Assert.That(await dbContext.BuildingTypes.AnyAsync(x => x.Name == buildingTypeViewModel.Name),
                Is.True);
        }

        [Test]
        public async Task Add_New_Country_Method_Should_Return_True_If_DB_Alread_Has_The_Country()
        {
            //Arrange
            SetUp();

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
            var result = await adminService.AddNewCountryAsync(countryModel);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Add_New_Country_Method_Should_Return_False_If_DB_Does_Not_Have_The_Country()
        {
            //Arange
            SetUp();

            var mockedCountryService = new Mock<ICountryService>();
            var repository = new Repository(dbContext);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            var countryModel = new AddNonExistingCountryViewModel()
            {
                Name = "Test"
            };

            //Act
            var result = await adminService.AddNewCountryAsync(countryModel);

            //Assert
            Assert.That(result, Is.False);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            //dbContext.Dispose();
        }
    }
}