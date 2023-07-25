namespace HomeXplorer.Tests.ProfileTests
{
    using Moq;

    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using HomeXplorer.Services.Exceptions;
    using NUnit.Framework.Internal.Execution;

    public class ProfileServiceTests
        : BaseTestsOptions
    {
        private IProfileService ps;
        private Mock<IGuard> guard;

        [SetUp]
        public void Initial()
        {
            guard = new Mock<IGuard>();
            ps = new ProfileService(dbContext, guard.Object);
        }

        [Test]
        public async Task Get_Agent_Profile_Info_Should_Return_Correct_Data()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
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
                Properties = new List<Property>()
                {
                    new Property()
                    {
                        Id = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                        Name = "TestName2",
                        Address = "TestAddress2",
                        Description = "TestDescription2",
                        CityId = 1,
                        Price = 10,
                        Size = 5,
                        PropertyStatusId = 1,
                        BuildingTypeId = 1,
                        AgentId = 1,
                        Images = new List<CloudImage>()
                        {
                            new CloudImage()
                            {
                                Id = 1,
                                PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                                Url = "test.test"
                            }
                        },
                    }
                },
                ProfilePictureUrl = "test@test"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await ps.GetAgentProfileInfoAsync(user.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.TotalUploadedProperties, Is.GreaterThan(0));
                Assert.That(await dbContext.Properties.CountAsync(),
                            Is.EqualTo(result.TotalUploadedProperties));
                Assert.That(await dbContext.Cities
                    .AnyAsync(c => c.Name == result.City), Is.True);
                Assert.That(await dbContext.Countries
                    .AnyAsync(c => c.Name == result.Country), Is.True);
                Assert.That(await dbContext.CloudImages.CountAsync(),
                    Is.EqualTo(result.PropertyImages.Count()));
            });
        }

        [Test]
        public async Task Get_Agent_Profile_Info_Should_Throw_Exception_When_User_Is_Null()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser user2 = new()
            {
                Id = "7298c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            //Act
            guard.Setup(g => g.AgainstNull(It.IsAny<Agent>(), "No agent was found"))
            .Throws(new HomeXplorerException("No agent was found"));

            //Assert
            Assert.ThrowsAsync<HomeXplorerException>(async () =>
                await ps.GetAgentProfileInfoAsync(user2.Id));
        }

        [Test]
        public async Task Get_Renter_Profile_Info_Should_Return_Correct_Data()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            Renter renter = new()
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
                ProfilePictureUrl = "test.test",
                Reviews = new List<Review>()
                {
                    new Review()
                    {
                        Id = 1,
                        Description = "TestTestTestTest1",
                        ReviewCreatorId = 1,
                    },
                    new Review()
                    {
                        Id = 2,
                        Description = "TestTestTestTest2",
                        ReviewCreatorId = 1
                    }
                },
                RentedProperties = new List<Property>()
                {
                    new Property()
                    {
                        Id = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                        Name = "TestName2",
                        Address = "TestAddress2",
                        Description = "TestDescription2",
                        CityId = 1,
                        Price = 10,
                        Size = 5,
                        PropertyStatusId = 1,
                        BuildingTypeId = 1,
                        AgentId = 1,
                        Images = new List<CloudImage>()
                        {
                            new CloudImage()
                            {
                                Id = 1,
                                PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                                Url = "test.test"
                            }
                        },
                        RenterId = 1
                    }
                }
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await ps.GetRenterProfileInfoAsync(user.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.TotalRentedProperties, Is.GreaterThan(0));
                Assert.That(await dbContext.Properties.CountAsync(p => p.RenterId == renter.Id),
                    Is.EqualTo(result.TotalRentedProperties));
                Assert.That(await dbContext.Cities
                    .AnyAsync(c => c.Name == result.City), Is.True);
                Assert.That(await dbContext.Countries
                    .AnyAsync(c => c.Name == result.Country), Is.True);
                Assert.That(await dbContext.Reviews.CountAsync(),
                    Is.EqualTo(result.TotalReviews));
            });
        }

        [Test]
        public async Task Get_Renter_Profile_Info_Should_Throw_Exception_When_Renter_Is_Null()
        {
            //Arrange
            ApplicationUser user = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser user2 = new()
            {
                Id = "7298c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            //Act
            guard.Setup(g => g.AgainstNull(It.IsAny<Renter>(), "No renter was found"))
            .Throws(new HomeXplorerException("No renter was found"));

            //Assert
            Assert.ThrowsAsync<HomeXplorerException>(async () =>
                await ps.GetRenterProfileInfoAsync(user2.Id));
        }
    }
}
