namespace HomeXplorer.Tests.PropertyStatusTests
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;

    public class PropertyStatusServiceTests
        : BaseTestsOptions
    {
        private IPropertyStatusService pts;

        [SetUp]
        public void Setup()
        {
            pts = new PropertyStatusService(dbContext);
        }

        [Test]
        public void Property_Status_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            //Act & Assert
            Assert.That(pts, Is.Not.Null);
            Assert.That(pts, Is.TypeOf<PropertyStatusService>());
            Assert.That(pts, Is.InstanceOf<IPropertyStatusService>());
        }

        [Test]
        public async Task Get_Property_Statuses_Method_Should_Return_Correct_Data()
        {
            //Arrange
            var propertyStatuses = new[]
            {
                new PropertyStatus() {Id = 1, Name = "Test1"},
                new PropertyStatus() {Id = 2, Name = "Test2"},
            };

            await dbContext.AddRangeAsync(propertyStatuses);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await pts.GetPropertyStatusesAsync();

            var mappedDbContextPropertyStatusesNames = dbContext.PropertyStatuses
                .Select(c => c.Name)
                .ToList();

            var mappedServicePropertyStatusesNames = result
                .Select(c => c.Name)
                .ToList();

            var mappedDbContextPropertyStatusesIds = dbContext.PropertyStatuses
                .Select(c => c.Id)
                .ToList();

            var mappedServicePropertyStatusesIds = result
                .Select(c => c.Id)
                .ToList();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result.Count(), Is.EqualTo(propertyStatuses.Length));
                Assert.That(mappedServicePropertyStatusesNames, Is.EqualTo(mappedDbContextPropertyStatusesNames));
                Assert.That(mappedServicePropertyStatusesNames, Is.EqualTo(mappedDbContextPropertyStatusesNames));
            });
        }
    }
}
