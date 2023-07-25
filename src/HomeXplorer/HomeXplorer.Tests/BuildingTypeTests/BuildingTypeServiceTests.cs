namespace HomeXplorer.Tests.BuildingTypeTests
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;

    public class BuildingTypeServiceTests
        : BaseTestsOptions
    {
        private IBuildingTypeService bts;

        [SetUp]
        public void Initial()
        {
            bts = new BuildingTypeService(dbContext);
        }

        [Test]
        public void Building_Type_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
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
    }
}
