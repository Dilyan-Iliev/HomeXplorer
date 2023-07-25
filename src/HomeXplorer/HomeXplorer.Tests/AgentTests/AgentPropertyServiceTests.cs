namespace HomeXplorer.Tests.AgentTests
{
    using Moq;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.ViewModels.Property.Agent;
    using Microsoft.EntityFrameworkCore;

    public class AgentPropertyServiceTests
        : BaseTestsOptions
    {
        private IAgentPropertyService aps;
        private Mock<IGuard> guard;
        private Mock<IRepository> repo;

        [SetUp]
        public void Initial()
        {
            guard = new Mock<IGuard>();
            repo = new Mock<IRepository>();
            aps = new AgentPropertyService(dbContext, guard.Object, repo.Object);
        }

        [Test]
        public void Agent_Property_Service_Should_Be_Successfully_Initialized_With_Its_Dependencies()
        {
            Assert.That(aps, Is.Not.Null);
            Assert.That(aps, Is.TypeOf<AgentPropertyService>());
            Assert.That(aps, Is.InstanceOf<IAgentPropertyService>());
        }

        [Test]
        public async Task Add_Method_Should_Successfully_Add_New_Property()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov"
            };

            Agent agent = new()
            {
                Id = 1,
                UserId = user.Id,
                City = new City()
                {
                    Id = 1,
                    Name = "Test",
                    Country = new Country()
                    {
                        Id = 1,
                        Name = "Test",
                    }
                },
                ProfilePictureUrl = "testPicture"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var propType = new PropertyType() { Id = 1, Name = "testType" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            AddPropertyViewModel model = new()
            {
                Name = "testName",
                Description = "testDescription",
                Price = 10,
                Size = 15,
                Address = "testAddress",
                PropertyStatusId = propStatus.Id,
                PropertyTypeId = propType.Id,
                BuildingTypeId = buildType.Id,
                CityId = 1,
                CountryId = 1,
            };

            var imageUrls = new List<string>() { "test1", "test2" };

            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.SaveChangesAsync();

            //Act
            await aps.AddAsync(model, imageUrls, user.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                Assert.That(await dbContext.Properties.CountAsync(), Is.GreaterThan(0));
                Assert.That(await dbContext.CloudImages.CountAsync(), Is.GreaterThan(0));
            });
        }
    }
}
