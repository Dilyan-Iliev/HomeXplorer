namespace HomeXplorer.Tests.AdminTests
{
    using Microsoft.EntityFrameworkCore;

    using Moq;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.ViewModels.Admin;

    public class AdminServiceTests
    {
        private static DbContextOptions<HomeXplorerDbContext> GetInMemoryDbOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<HomeXplorerDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        [Test]
        public async Task Get_All_Building_Types_Method_Should_Return_Correct_Data()
        {
            var options = GetInMemoryDbOptions("TestDataBase");

            // Set up an in-memory database context with the options
            var context = new HomeXplorerDbContext(options);
            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Luxury"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Ordinary"},
            };

            // Seed the in-memory database with test data
            context.BuildingTypes.AddRange(buildingTypes);
            await context.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            // Use the real repository with the in-memory database context
            var repository = new Repository(context);

            var adminService = new AdminService(repository, mockedCountryService.Object);

            // Act
            var result = await adminService.GetAllBuildingTypesAsync();
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(result, Is.Not.EqualTo(null));
                Assert.That(result,
                    Is.EqualTo(buildingTypes.Select(bt => bt.Name)));
            });
        }

        [Test]
        public async Task Add_New_Building_Type_Method_Should_Return_True_If_DB_Already_Has_The_Building_Type()
        {
            //Arrange
            var options = GetInMemoryDbOptions("TestDataBase");

            var context = new HomeXplorerDbContext(options);
            var buildingTypes = new List<BuildingType>
            {
                new BuildingType() {Name = "Test"},
                new BuildingType() {Name = "Average"},
                new BuildingType() {Name = "Test2"},
            };

            context.BuildingTypes.AddRange(buildingTypes);
            await context.SaveChangesAsync();

            var mockedCountryService = new Mock<ICountryService>();

            // Use the real repository with the in-memory database context
            var repository = new Repository(context);

            //Act
            var adminService = new AdminService(repository, mockedCountryService.Object);

            //Assert
            var buildingTypeModel = new AddNonExistingBuildingTypeViewModel()
            {
                Name = "Average"
            };

            var result = await adminService.AddNewBuildingTypeAsync(buildingTypeModel);

            Assert.IsTrue(result);
        }
    }
}