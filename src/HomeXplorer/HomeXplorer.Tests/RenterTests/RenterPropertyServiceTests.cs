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

        [Test]
        public async Task Get_Property_Details_Method_Should_Return_Correct_Data()
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
                    {Id = 1,Description = "Test1",ReviewCreatorId = 1}
                }
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };
            var propType = new PropertyType() { Id = 1, Name = "testStatus" };

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
                PropertyTypeId = propType.Id,
                AgentId = 1,
                RenterId = renter.Id,
                Images = new List<CloudImage>()
                {
                    new CloudImage()
                    {Id = 1,PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),Url = "test.test"},
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
                    { Id = 2, Name = "Test2" }
                },
                Properties = new List<Property>() { property },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await rps.GetPropertyDetailsAsync(property.Id, renterUser.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(property.Id));
                Assert.That(result.Name, Is.EqualTo(property.Name));
                Assert.That(result.Description, Is.EqualTo(property.Description));
                Assert.That(result.Address, Is.EqualTo(property.Address));
                Assert.That(result.City, Is.EqualTo(property.City.Name));
                Assert.That(result.Country, Is.EqualTo(property.City.Country.Name));
                Assert.That(result.Price, Is.EqualTo(property.Price));
                Assert.That(result.Size, Is.EqualTo(property.Size));
                Assert.That(result.AddedOd, Is.EqualTo(property.AddedOn.ToString("MM/dd/yyyy")));
                Assert.That(result.PropertyType, Is.EqualTo(property.PropertyType.Name));
                Assert.That(result.PropertyStatus, Is.EqualTo(property.PropertyStatus.Name));
                Assert.That(result.BuildingType, Is.EqualTo(property.BuildingType.Name));
                Assert.That(result.IsRented, Is.EqualTo(property.RenterId != null));
                Assert.That(result.Images.Count(), Is.EqualTo(property.Images.Count));
                Assert.That(result.AgentEmail, Is.EqualTo(property.Agent.User.Email));
                Assert.That(result.AgentPhone, Is.EqualTo(property.Agent.User.PhoneNumber));
                Assert.That(result.AgentFullName,
                    Is.EqualTo($"{property.Agent.User.FirstName} {property.Agent.User.LastName}"));
                Assert.That(result.AgentProfilePicture, Is.EqualTo(property.Agent.ProfilePictureUrl));
            });
        }

        [Test]
        public async Task Get_Property_Details_Method_Should_Return_Null_If_No_Property_Was_Found()
        {
            //Arrange
            ApplicationUser agentUser = new()
            {
                Id = "5898c6a7-da15-4f3b-abfd-16cdd14ca50c",
                FirstName = "Test",
                LastName = "Testov"
            };

            Property property = new()
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
                PropertyTypeId = 1,
                AgentId = 1,
                Images = new List<CloudImage>()
                {
                    new CloudImage()
                    {Id = 1,PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),Url = "test.test"},
                }
            };

            Property property2 = new()
            {
                Id = Guid.Parse("64ee63f0-f5e5-4f93-ad64-bfff3c0886a2"),
                Name = "TestName2",
                Address = "TestAddress2",
                Description = "TestDescription2",
                CityId = 1,
                Price = 10,
                Size = 5,
                PropertyStatusId = 1,
                BuildingTypeId = 1,
                PropertyTypeId = 1,
                AgentId = 2,
                Images = new List<CloudImage>()
                {
                    new CloudImage()
                    {Id = 2,PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),Url = "test.test"},
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
                    { Id = 2, Name = "Test2" }
                },
                Properties = new List<Property>() { property },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await rps.GetPropertyDetailsAsync(property2.Id, agentUser.Id);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Rent_Method_Should_Increse_Rented_Properties_Of_Renter_User()
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
                    {Id = 1,Description = "Test1",ReviewCreatorId = 1}
                }
            };

            var propStatus = new PropertyStatus() { Id = 1, Name = "Free" };
            var buildType = new BuildingType() { Id = 1, Name = "testBuild" };
            var propType = new PropertyType() { Id = 1, Name = "testStatus" };

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
                PropertyTypeId = propType.Id,
                AgentId = 1,
                Images = new List<CloudImage>()
                {
                    new CloudImage()
                    {Id = 1,PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),Url = "test.test"},
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
                    { Id = 2, Name = "Test2" }
                },
                Properties = new List<Property>() { property },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.SaveChangesAsync();

            //Act
            await rps.RentAsync(property.Id, renterUser.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                Assert.That(property.PropertyStatus.Name, Is.EqualTo("Taken"));
                Assert.That(property.RenterId, Is.Not.Null);
                Assert.That(property.RenterId, Is.EqualTo(renter.Id));
                Assert.That(renter!.RentedProperties!.Count,
                    Is.EqualTo(await dbContext
                    .Properties.CountAsync(p => p.RenterId == renter.Id)));
            });
        }

        [Test]
        public async Task Leave_Method_Should_Decrease_Rented_Properties_Of_Renter_User()
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
            var propType = new PropertyType() { Id = 1, Name = "testStatus" };

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
                PropertyTypeId = propType.Id,
                AgentId = 1,
                Images = new List<CloudImage>()
                {
                    new CloudImage()
                    {Id = 1,PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),Url = "test.test"},
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
                    {Id = 1,Description = "Test1",ReviewCreatorId = 1}
                },
                RentedProperties = new List<Property>() { property }
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
                    { Id = 2, Name = "Test2" }
                },
                Properties = new List<Property>() { property },
                ProfilePictureUrl = "testPicture",
            };

            await dbContext.Users.AddAsync(agentUser);
            await dbContext.Users.AddAsync(renterUser);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.Renters.AddAsync(renter);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.SaveChangesAsync();

            //Act
            await rps.LeaveAsync(property.Id, renterUser.Id);

            //Assert
            Assert.Multiple(async () =>
            {
                Assert.That(property.PropertyStatus.Name, Is.EqualTo("Free"));
                Assert.That(property.RenterId, Is.Null);
                Assert.That(renter!.RentedProperties!.Count,
                    Is.EqualTo(await dbContext
                    .Properties.CountAsync(p => p.RenterId == renter.Id)));
            });
        }
    }
}
