namespace HomeXplorer.Tests.BuildingTypeTests
{
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class BuildingTypeServiceTests
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
        public void Building_Type_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Arrange
            IBuildingTypeService bts = new BuildingTypeService(dbContext);

            //Act & Assert
            Assert.That(bts, Is.Not.Null);
            Assert.That(bts, Is.TypeOf<BuildingTypeService>());
            Assert.That(bts, Is.InstanceOf<IBuildingTypeService>());
        }

        [Test]
        public async Task Get_Building_Types_Method_Should_Return_Correct_Data()
        {
            //Arrange
            var buildingTypes = new[]
            {
                new BuildingType() {Id = 1, Name = "Test1"},
                new BuildingType() {Id = 2, Name = "Test2"},
            };

            await dbContext.AddRangeAsync(buildingTypes);
            await dbContext.SaveChangesAsync();

            var bts = new BuildingTypeService(dbContext);

            //Act
            var result = await bts.GetBuildingTypesAsync();

            var mappedDbContextBuildingTypesNames = dbContext.BuildingTypes
                .Select(c => c.Name)
                .ToList();

            var mappedServiceBuildingTypesNames = result
                .Select(c => c.Name)
                .ToList();

            var mappedDbContextBuildingTypesIds = dbContext.BuildingTypes
                .Select(c => c.Id)
                .ToList();

            var mappedServiceBuildingTypesIds = result
                .Select(c => c.Id)
                .ToList();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.Count, Is.EqualTo(buildingTypes.Length));
                Assert.That(mappedServiceBuildingTypesNames, Is.EqualTo(mappedDbContextBuildingTypesNames));
                Assert.That(mappedServiceBuildingTypesNames, Is.EqualTo(mappedDbContextBuildingTypesNames));
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
