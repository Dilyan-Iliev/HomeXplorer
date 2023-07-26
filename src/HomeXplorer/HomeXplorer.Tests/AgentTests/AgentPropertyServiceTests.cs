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

        [Test]
        public async Task Delete_Method_Should_Change_Property_Is_Active_Status_To_False_If_Property_Is_Not_Null()
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

            Property property = new()
            {
                Id = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                Name = "TestName",
                Address = "TestAddress",
                Description = "TestDescription",
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
                    {
                        Id = 1,
                        PropertyId = Guid.Parse("63ee63f0-f5e5-4f93-ad53-afff3c0886a2"),
                        Url = "test.test"
                    }
                }
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.PropertyStatuses.AddAsync(propStatus);
            await dbContext.Properties.AddAsync(property);
            await dbContext.SaveChangesAsync();

            //Act
            await aps.DeleteAsync(property.Id);

            //Assert
            Assert.That(property.IsActive, Is.False);
        }

        [Test]
        public async Task Edit_Method_Should_Change_Successfully_Old_Property_Info()
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
                        Url = "test.test"
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
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };


            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            var imageUrls = new List<string>() { "test1", "test2" };
            var model = new EditPropertyViewModel()
            {
                Price = 15,
                Size = 25,
                Name = "TestEditedName",
                Description = "TestEditedDescription",
            };
            ICollection<CloudImage> oldImages = property.Images;
            var deletedPhotosIds = new List<int>() { 1 };

            //Act
            await aps.EditAsync(model, property.Id, imageUrls, oldImages, deletedPhotosIds);

            //Assert
            Assert.Multiple(async () =>
            {

                Assert.That(property.Price, Is.EqualTo(model.Price));
                Assert.That(property.Size, Is.EqualTo(model.Size));
                Assert.That(property.Name, Is.EqualTo(model.Name));
                Assert.That(property.Description, Is.EqualTo(model.Description));

                if (imageUrls != null && imageUrls.Any() &&
                (deletedPhotosIds != null && deletedPhotosIds.Count == 0))
                {
                    Assert.That(property.Images,
                        Has.Count.EqualTo(oldImages.Count + imageUrls.Count));
                }
                else
                {
                    Assert.That(property.Images, Is.EqualTo(oldImages));
                }

                if (deletedPhotosIds == null && imageUrls == null)
                {
                    Assert.That(property.Images, Has.Count.EqualTo(await dbContext
                        .CloudImages
                        .CountAsync(ci => ci.PropertyId == property.Id)));
                }

                if (deletedPhotosIds?.Any() ?? false)
                {
                    foreach (var photoId in deletedPhotosIds)
                    {
                        Assert.That(property.Images,
                            Has.None.Matches<CloudImage>(ci => ci.Id == photoId));
                    }
                }
            });
        }

        [Test]
        public async Task Find_By_Id_Method_Should_Return_Property_Model_If_One_Was_Found()
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
                        Url = "test.test"
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
                Properties = new List<Property>()
                {
                    property
                },
                ProfilePictureUrl = "testPicture",
            };


            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await aps.FindByIdAsync(property.Id);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task Find_By_Id_Method_Should_Return_Null_If_No_Property_Was_Found()
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
                        Url = "test.test"
                    }
                }
            };

            var property2 = new Property()
            {
                Id = Guid.Parse("69ee63f0-f5e5-4f93-ad54-bfff3c0886a2"),
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
                    property
                },
                ProfilePictureUrl = "testPicture",
            };


            await dbContext.Users.AddAsync(user);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.PropertyTypes.AddAsync(propType);
            await dbContext.BuildingTypes.AddAsync(buildType);
            await dbContext.SaveChangesAsync();

            //Act
            var result = await aps.FindByIdAsync(property2.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task Get_All_Image_Urls_For_Property_Should_Return_Correct_Data()
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
            await dbContext.SaveChangesAsync();

            //Act
            var result = await aps.GetAllImageUrlsForPropertyAsync(property.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(property.Images.Count));

            foreach (var img in result)
            {
                Assert.That(property.Images,
                             Has.Some.Matches<CloudImage>(ci => ci.Id == img.Id));
            }
        }

        [Test]
        public async Task Get_Details_Method_Should_Return_Correct_Data_When_Property_Was_Found_By_Id()
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
                PropertyTypeId = propType.Id,
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
            var result = await aps.GetDetailsAsync(property.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Id, Is.EqualTo(property.Id));
                Assert.That(result.Name, Is.EqualTo(property.Name));
                Assert.That(result.Description, Is.EqualTo(property.Description));
                Assert.That(result.Price, Is.EqualTo(property.Price));
                Assert.That(result.Size, Is.EqualTo(property.Size));
                Assert.That(result.Address, Is.EqualTo(property.Address));
                Assert.That(result.City, Is.EqualTo(property.City.Name));
                Assert.That(result.Country, Is.EqualTo(property.City.Country.Name));
                Assert.That(result.AddedOd, Is.EqualTo(property.AddedOn.ToString("MM/dd/yyyy")));
                Assert.That(result.PropertyStatus, Is.EqualTo(property.PropertyStatus.Name));
                Assert.That(result.PropertyType, Is.EqualTo(property.PropertyType.Name));
                Assert.That(result.BuildingType, Is.EqualTo(property.BuildingType.Name));
                Assert.That(result.Images.Count(), Is.EqualTo(property.Images.Count));
            });
        }

        [Test]
        public async Task Get_Details_Method_Should_Return_Null_When_No_Property_Was_Found_By_Id()
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
                PropertyTypeId = propType.Id,
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

            var property2 = new Property()
            {
                Id = Guid.Parse("83ee65f0-f6e5-4f93-ad53-afff3c0886a2"),
                Name = "TestName2",
                Address = "TestAddress2",
                Description = "TestDescription2",
                CityId = 1,
                Price = 10,
                Size = 5,
                PropertyStatusId = propStatus.Id,
                PropertyTypeId = propType.Id,
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
            var result = await aps.GetDetailsAsync(property2.Id);

            //Assert
            Assert.That(result, Is.Null);
        }


    }
}
