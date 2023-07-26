namespace HomeXplorer.Tests.RenterTests
{
    using Moq;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Entities;
    using Microsoft.EntityFrameworkCore;

    public class RenterPropertyServiceTests
        : BaseTestsOptions
    {
        private IRenterPropertyService rps;
        private Mock<IGuard> guard;

        [SetUp]
        public void Initial()
        {
            guard = new Mock<IGuard>();
            rps = new RenterPropertyService(guard.Object, dbContext);
        }

        [Test]
        public void Renter_Property_Service_Should_Be_Successfully_Initialized_With_Its_Dependenies()
        {
            Assert.That(rps, Is.Not.Null);
            Assert.That(rps, Is.TypeOf<RenterPropertyService>());
            Assert.That(rps, Is.InstanceOf<IRenterPropertyService>());
        }

        [Test]
        public async Task Add_To_Favorites_Method_Should_Increase_Renter_Favorite_Properties_When_The_Property_Was_Not_Already_Added()
        {
            //Arrange
            ApplicationUser renterUser = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser agentUser = new()
            {
                Id = "5898c6a7-da15-4f3b-abfd-16cdd14ca50c",
                FirstName = "Test",
                LastName = "Testov"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            Property property = new()
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
                        Url = "test.test"
                    }
                }
            };

            Renter renter = new()
            {
                Id = 1,
                UserId = renterUser.Id,
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
                }
            };

            Agent agent = new()
            {
                Id = 1,
                UserId = agentUser.Id,
                City = new City()
                {
                    Id = 2,
                    Name = "Test2",
                    Country = new Country()
                    {
                        Id = 2,
                        Name = "Test2",
                    }
                },
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act
            await rps.AddToFavoritesAsync(property.Id, renterUser.Id);

            //Assert
            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.GreaterThan(0));

            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.EqualTo(renter!.FavouriteProperties!.Count));
        }

        [Test]
        public async Task Add_To_Favorites_Method_Should__Not_Increase_Renter_Favorite_Properties_When_The_Property_Was_Already_Added()
        {
            //Arrange
            ApplicationUser renterUser = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser agentUser = new()
            {
                Id = "5898c6a7-da15-4f3b-abfd-16cdd14ca50c",
                FirstName = "Test",
                LastName = "Testov"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            Property property = new()
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
                        Url = "test.test"
                    }
                }
            };

            Renter renter = new()
            {
                Id = 1,
                UserId = renterUser.Id,
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
                }
            };

            Agent agent = new()
            {
                Id = 1,
                UserId = agentUser.Id,
                City = new City()
                {
                    Id = 2,
                    Name = "Test2",
                    Country = new Country()
                    {
                        Id = 2,
                        Name = "Test2",
                    }
                },
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act

            //Add new property to favs:
            await rps.AddToFavoritesAsync(property.Id, renterUser.Id);
            //Try add already added property second time: should not increase favorites count
            await rps.AddToFavoritesAsync(property.Id, renterUser.Id);

            //Assert
            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.GreaterThan(0));

            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.EqualTo(1));
        }

        [Test]
        public async Task Remove_From_Favorites_Method_Should_Decrease_Favorite_Properties_Count()
        {
            //Arrange
            ApplicationUser renterUser = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser agentUser = new()
            {
                Id = "5898c6a7-da15-4f3b-abfd-16cdd14ca50c",
                FirstName = "Test",
                LastName = "Testov"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            Property property = new()
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
                        Url = "test.test"
                    }
                }
            };

            Renter renter = new()
            {
                Id = 1,
                UserId = renterUser.Id,
                City = new City()
                {
                    Id = 1,
                    Name = "Test",
                    Country = new Country()
                    { Id = 1, Name = "Test" }
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
                }
            };

            var favProperty1 = new RenterPropertyFavorite()
            {
                PropertyId = property.Id,
                RenterId = renter.Id
            };

            Agent agent = new()
            {
                Id = 1,
                UserId = agentUser.Id,
                City = new City()
                {
                    Id = 2,
                    Name = "Test2",
                    Country = new Country()
                    {
                        Id = 2,
                        Name = "Test2",
                    }
                },
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.RentersPropertiesFavorites.AddAsync(favProperty1);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act
            await rps.RemoveFromFavoritesAsync(property.Id, renterUser.Id);

            //Assert
            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.EqualTo(0));

            Assert.That(await dbContext.RentersPropertiesFavorites
                .CountAsync(rpf => rpf.RenterId == renter.Id
                && rpf.PropertyId == property.Id), Is.EqualTo(renter!.FavouriteProperties!.Count));
        }

        [Test]
        public async Task Remove_From_Favorites_Method_Should_Not_Remove_If_Favorite_Property_Is_Null()
        {
            //Arrange
            ApplicationUser renterUser = new()
            {
                Id = "7898c6a7-da15-4f3b-abfd-16cdd74ca80a",
                FirstName = "Test",
                LastName = "Testov",
                Email = "test@abv.bg",
                PhoneNumber = "000"
            };

            ApplicationUser agentUser = new()
            {
                Id = "5898c6a7-da15-4f3b-abfd-16cdd14ca50c",
                FirstName = "Test",
                LastName = "Testov"
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };

            Property property = new()
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
                        Url = "test.test"
                    }
                }
            };

            Renter renter = new()
            {
                Id = 1,
                UserId = renterUser.Id,
                City = new City()
                {
                    Id = 1,
                    Name = "Test",
                    Country = new Country()
                    { Id = 1, Name = "Test" }
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
                }
            };

            Agent agent = new()
            {
                Id = 1,
                UserId = agentUser.Id,
                City = new City()
                {
                    Id = 2,
                    Name = "Test2",
                    Country = new Country()
                    {
                        Id = 2,
                        Name = "Test2",
                    }
                },
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act
            await rps.RemoveFromFavoritesAsync(property.Id, renterUser.Id);

            //Assert
            Assert.That(renter.FavouriteProperties, Is.Not.Null);
            Assert.That(renter.FavouriteProperties, Is.Empty);
        }


    }
}
