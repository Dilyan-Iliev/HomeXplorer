namespace HomeXplorer.Tests.BaseHomePropertyTests
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.ViewModels.Property.Renter;

    public class HomePropertyServiceTests
        : BaseTestsOptions
    {
        private IHomePropertyService hps;

        [SetUp]
        public void Initiall()
        {
            hps = new HomePropertyService(dbContext);
        }

        [Test]
        public async Task GetAllPropertiesAsync_Should_Return_Valid_Result()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var propType = new PropertyType() { Id = 1, Name = "testType" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            var property = new Property()
            {
                Id = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                Name = "TestName2",
                Address = "TestAddress2",
                Description = "TestDescription2",
                CityId = 1,
                Price = 10,
                Size = 5,
                PropertyStatusId = propStatus.Id,
                BuildingTypeId = buildType.Id,
                AgentId = 1,
                Images = new List<CloudImage>()
                    {
                        new CloudImage()
                        {
                            Id = 1,
                            PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                            Url = "test.test1"
                        },
                        new CloudImage()
                        {
                            Id = 2,
                            PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                            Url = "test.test2"
                        }
                    }
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
                Properties = new List<Property>() { property },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await hps.GetAllPropertiesAsync();
            var expectedProperty = property;
            var actualProperty = result.First();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count(), Is.GreaterThan(0));
                Assert.That(result, Is.TypeOf<List<LatestPropertiesViewModel>>());
                Assert.That(actualProperty.Id, Is.EqualTo(expectedProperty.Id));
                Assert.That(actualProperty.Name, Is.EqualTo(expectedProperty.Name));
                Assert.That(actualProperty.Price, Is.EqualTo(expectedProperty.Price));
                Assert.That(actualProperty.Size, Is.EqualTo(expectedProperty.Size));
                Assert.That(actualProperty.City, Is.EqualTo(expectedProperty.City.Name));
                Assert.That(actualProperty.Status, Is.EqualTo(expectedProperty.PropertyStatus.Name));
                Assert.That(actualProperty.AddedOn, Is.EqualTo(expectedProperty.AddedOn.ToString("MM/dd/yyyy")));
                Assert.That(actualProperty.CoverImageUrl, Is.EqualTo(expectedProperty.Images.ElementAt(0).Url));
            });
        }
    }
}
