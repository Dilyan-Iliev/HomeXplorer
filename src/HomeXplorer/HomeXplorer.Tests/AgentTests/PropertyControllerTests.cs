namespace HomeXplorer.Tests.AgentTests
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;
    using CloudinaryDotNet;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.Services.Exceptions;
    using HomeXplorer.Areas.Agent.Controllers;
    using HomeXplorer.ViewModels.PropertyType;
    using HomeXplorer.ViewModels.BuildingType;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.Common;

    public class PropertyControllerTests
    {
        private IConfiguration config;
        private Mock<IPropertyTypeService> propertyTypeService;
        private Mock<ICountryService> countryService;
        private Mock<IBuildingTypeService> buildingTypeService;
        private Mock<IAgentPropertyService> agentPropService;
        private Mock<IPropertyStatusService> propertyStatusService;
        private Mock<IRepository> repo;
        private Mock<ICloudinaryService> cloudinaryService;
        private Mock<Cloudinary> cloudinary;

        private ClaimsPrincipal userPrincipal;
        private HttpContext context;

        [SetUp]
        public void SetUp()
        {
            config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddUserSecrets<PropertyControllerTests>()
            .Build();

            string cloud = config["Cloudinary:cloud_name"];
            string apiKey = config["Cloudinary:api_key"];
            string apiSecret = config["Cloudinary:api_secret"];

            var account = new Account(cloud, apiKey, apiSecret);

            propertyTypeService = new Mock<IPropertyTypeService>();
            countryService = new Mock<ICountryService>();
            buildingTypeService = new Mock<IBuildingTypeService>();
            agentPropService = new Mock<IAgentPropertyService>();
            propertyStatusService = new Mock<IPropertyStatusService>();
            repo = new Mock<IRepository>();
            cloudinaryService = new Mock<ICloudinaryService>();
            cloudinary = new Mock<Cloudinary>(account);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "testuser"),
                new Claim(ClaimTypes.Email, "testuser@example.com"),
                new Claim(ClaimTypes.NameIdentifier, "testuser")
            };

            var identity = new ClaimsIdentity(claims, "TestAuthentication");

            userPrincipal = new ClaimsPrincipal(identity);

            // Set the ClaimsPrincipal in the HttpContext
            context = CreateHttpContext(userPrincipal);
        }

        [Test]
        public async Task All_Should_Return_ViewResult_With_Correct_Model()
        {
            //Arrange
            var model = new AgentAllPropertiesViewModel()
            {
                PageNumber = 1,
                PageSize = 6,
                PropertySorting = PropertySorting.Default,
                TotalPages = 3,
            };

            agentPropService.Setup(aps => aps.AllAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<PropertySorting>(), It.IsAny<string>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.All();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AgentAllPropertiesViewModel>());
        }

        [Test]
        public async Task All_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new AgentAllPropertiesViewModel();

            agentPropService.Setup(aps => aps.AllAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<PropertySorting>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.All();

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        public async Task HttpGet_Add_Should_Return_ViewData_With_Correct_Model()
        {
            var countries = new List<SelectCountryViewModel>
            {
                new SelectCountryViewModel() {Id = 1, Name = "Test1"},
                new SelectCountryViewModel() {Id = 2, Name = "Test2"},
            };

            var propertyTypes = new List<SelectPropertyTypeViewModel>
            {
                new SelectPropertyTypeViewModel() {Id = 1, Name = "Test1"},
                new SelectPropertyTypeViewModel() {Id = 2, Name = "Test2"},
            };

            var buildingTypes = new List<SelectBuildingTypeViewModel>
            {
                new SelectBuildingTypeViewModel() { Id = 1, Name = "Test1"},
                new SelectBuildingTypeViewModel() { Id = 2, Name = "Test2"},
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(countries);

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(propertyTypes);

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(buildingTypes);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Add();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AddPropertyViewModel>());

            var model = viewResult.Model as AddPropertyViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(model!.Countries, Is.Not.Null);
                Assert.That(model!.Countries, Is.EqualTo(countries));

                Assert.That(model!.PropertyTypes, Is.Not.Null);
                Assert.That(model!.PropertyTypes, Is.EqualTo(propertyTypes));

                Assert.That(model!.BuildingTypes, Is.Not.Null);
                Assert.That(model!.BuildingTypes, Is.EqualTo(buildingTypes));
            });
        }

        [Test]
        public async Task HttpGet_Add_Should_Return_TempDataView_On_Exception()
        {
            countryService.Setup(cs => cs.GetCountriesAsync())
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Add();

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        [TestCase(null, null, 249, 39, null, null, null, null, null, null, null)]
        [TestCase(null, null, 100_000, 500, null, null, null, null, null, null, null)]
        [TestCase("Qwertyuio", "Qwertyuiopasdfghjklz", 250, 40, "Qwertyuiopasdfg", 1, 1, 1, 1, 1, null)]
        [TestCase("Qwertyuiop", "Qwertyuiopasdfghj", 100000, 500, "Qwertyuiopasdfg", 1, 1, 1, 1, 1, null)]
        [TestCase("Qwertyuiop", "Qwertyuiopasdfghjklz", 250, 40, "Qwertyuiopasd", 1, 1, 1, 1, 1, null)]
        public async Task HttpPost_Add_On_Invalid_Model_Should_Return_Same_View_With_Model(string name,
            string description, decimal price, int size, string address, int? countryId, int? cityId,
            int? propertyTypeId, int? propertyStatusId, int? buildingTypeId, ICollection<IFormFile>? images)
        {
            //Arrange
            var model = new AddPropertyViewModel()
            {
                Name = name,
                Description = description,
                Price = price,
                Size = size,
                Address = address,
                CountryId = countryId ?? 0,
                CityId = cityId ?? 0,
                PropertyTypeId = propertyTypeId ?? 0,
                PropertyStatusId = propertyStatusId ?? 0,
                BuildingTypeId = buildingTypeId ?? 0,
                Images = images!
            };

            agentPropService.Setup(aps => aps.AddAsync(model, It.IsAny<ICollection<string>>(),
                It.IsAny<string>()))
                .Verifiable();

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description)
                || string.IsNullOrEmpty(address) || countryId == null
                || cityId == null || propertyTypeId == null || propertyStatusId == null
                || buildingTypeId == null || images == null)
            {
                controller.ModelState.AddModelError("Name", "All fields are required");
            }
            else if (price < 250 || price > 100000)
            {
                controller.ModelState.AddModelError("Price", "Price field must be between 250 and 100000");
            }
            else if (size < 40 || size > 500)
            {
                controller.ModelState.AddModelError("Size", "Size field must be between 40 and 500");
            }

            //Act
            var result = await controller.Add(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task HttpPost_Add_Should_Return_ViewResult_With_TempDataMessage_On_Invalid_DropdownOption()
        {
            var model = new AddPropertyViewModel()
            {
                Name = "Test Name",
                Description = "Test Description",
                Price = 125,
                Size = 59,
                Address = "Test Address",
                CountryId = -1,
                CityId = -2,
                PropertyTypeId = 0,
                PropertyStatusId = 0,
                BuildingTypeId = -1,
                Images = null!
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(new List<SelectPropertyTypeViewModel>());

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(new List<SelectBuildingTypeViewModel>());

            agentPropService.Setup(aps => aps.ExistByIdAsync<Country>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<City>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<PropertyType>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<BuildingType>(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Add(model);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(controller.TempData["InvalidDropdownOption"],
                    Is.EqualTo("You must choose a valid option from the dropdowns"));
            });
        }

        [Test]
        public async Task HttpPost_Add_Should_Return_Correct_Redirection_On_Valid_Model_And_DropdownOptions()
        {
            var model = new AddPropertyViewModel()
            {
                Name = "Test Name",
                Description = "Test Description",
                Price = 125,
                Size = 59,
                Address = "Test Address",
                CountryId = 1,
                CityId = 2,
                PropertyTypeId = 1,
                PropertyStatusId = 1,
                BuildingTypeId = 1,
                Images = new List<IFormFile>()
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(new List<SelectPropertyTypeViewModel>());

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(new List<SelectBuildingTypeViewModel>());

            agentPropService.Setup(aps => aps.ExistByIdAsync<Country>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<City>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<PropertyType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<BuildingType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            cloudinaryService.Setup(cs => cs.UploadMany(cloudinary.Object,
                It.IsAny<ICollection<IFormFile>>()))
                .ReturnsAsync(new List<string>());

            agentPropService.Setup(aps => aps.AddAsync(model, It.IsAny<List<string>>(), It.IsAny<string>()))
                .Verifiable();

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Add(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectionResult!.ActionName, Is.EqualTo("Index"));
                Assert.That(redirectionResult!.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectionResult!.RouteValues!["area"], Is.EqualTo("Agent"));
            });
        }

        [Test]
        public async Task HttpPost_Add_Should_Return_ViewResult_On_InvalidFileExtensionException()
        {
            var model = new AddPropertyViewModel()
            {
                Name = "Test Name",
                Description = "Test Description",
                Price = 125,
                Size = 59,
                Address = "Test Address",
                CountryId = 1,
                CityId = 2,
                PropertyTypeId = 1,
                PropertyStatusId = 1,
                BuildingTypeId = 1,
                Images = new List<IFormFile>(),
                Errors = new List<string>() { "Not allowed file extension" }
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(new List<SelectPropertyTypeViewModel>());

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(new List<SelectBuildingTypeViewModel>());

            agentPropService.Setup(aps => aps.ExistByIdAsync<Country>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<City>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<PropertyType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<BuildingType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            cloudinaryService.Setup(cs => cs.UploadMany(cloudinary.Object,
                It.IsAny<ICollection<IFormFile>>()))
                .ThrowsAsync(new InvalidFileExtensionException("Not allowed file extension"));

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Add(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            var resultModel = viewResult!.Model as AddPropertyViewModel;
            Assert.That(resultModel!.Errors!, Has.Count.EqualTo(model.Errors.Count));
            Assert.That(resultModel!.Errors!.ElementAt(0),
                Is.EqualTo("Not allowed file extension"));
        }

        [Test]
        public async Task HttpPost_Add_Should_Return_TempDataView_On_Exceptions_Different_Than_InvalidFileExtensionException()
        {
            var model = new AddPropertyViewModel()
            {
                Name = "Test Name",
                Description = "Test Description",
                Price = 125,
                Size = 59,
                Address = "Test Address",
                CountryId = 1,
                CityId = 2,
                PropertyTypeId = 1,
                PropertyStatusId = 1,
                BuildingTypeId = 1,
                Images = new List<IFormFile>(),
                Errors = new List<string>() { "Not allowed file extension" }
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(new List<SelectPropertyTypeViewModel>());

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(new List<SelectBuildingTypeViewModel>());

            agentPropService.Setup(aps => aps.ExistByIdAsync<Country>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<City>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<PropertyType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.ExistByIdAsync<BuildingType>(It.IsAny<int>()))
                .ReturnsAsync(true);

            agentPropService.Setup(aps => aps.AddAsync(model, It.IsAny<ICollection<string>>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            cloudinaryService.Setup(cs => cs.UploadMany(cloudinary.Object,
                It.IsAny<ICollection<IFormFile>>()))
                .ReturnsAsync(new List<string>());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Add(model);

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        public async Task Details_Should_Return_ViewResult_With_Correct_Not_Null_Model()
        {
            //Arrange
            var model = new DetailsPropertyViewModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test Property Name",
                Description = "Test Property Description",
                Address = "Test Property Address",
                City = "Test Property City",
                Country = "Test Property Country",
                Size = 15,
                Price = 50,
                AddedOd = DateTime.UtcNow.ToString("MM/dd/yyyy"),
                PropertyType = "Test Property Type",
                PropertyStatus = "Test Property Status",
                BuildingType = "Test Building Type",
                Images = null!
            };

            agentPropService.Setup(aps => aps.GetDetailsAsync(model.Id))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Details(model.Id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<DetailsPropertyViewModel>());

            var detailsModel = viewResult!.Model as DetailsPropertyViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(detailsModel!.Id, Is.EqualTo(model.Id));
                Assert.That(detailsModel.Name, Is.EqualTo(model.Name));
                Assert.That(detailsModel.Size, Is.EqualTo(model.Size));
                Assert.That(detailsModel.City, Is.EqualTo(model.City));
                Assert.That(detailsModel.Price, Is.EqualTo(model.Price));
                Assert.That(detailsModel.Images, Is.EqualTo(model.Images));
                Assert.That(detailsModel.Country, Is.EqualTo(model.Country));
                Assert.That(detailsModel.AddedOd, Is.EqualTo(model.AddedOd));
                Assert.That(detailsModel.Address, Is.EqualTo(model.Address));
                Assert.That(detailsModel.Description, Is.EqualTo(model.Description));
            });
        }

        [Test]
        public async Task HttpPost_Should_Return_Correct_Redirection_On_Null_Model()
        {
            //Arrange
            DetailsPropertyViewModel? model = null;

            agentPropService.Setup(aps => aps.GetDetailsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Details(It.IsAny<Guid>());

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["DetailsError"],
                    Is.EqualTo("Can not show the details of the property"));
            });

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo("Index"));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectResult!.RouteValues!["area"],
                    Is.EqualTo("Agent"));
            });
        }

        [Test]
        public async Task Details_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new DetailsPropertyViewModel() { Id = Guid.NewGuid() };

            agentPropService.Setup(aps => aps.GetDetailsAsync(model.Id))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Details(model.Id);

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        public async Task Delete_Should_Return_Correct_Redirection_On_No_Exception()
        {
            //Arrange
            var property = new Property() { Id = Guid.NewGuid() };
            agentPropService.Setup(aps => aps.DeleteAsync(property.Id))
                .Verifiable();

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Delete(property.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["SuccessDelete"],
                    Is.EqualTo("You removed successfuly a property"));
            });

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo("Index"));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectResult!.RouteValues!["area"],
                    Is.EqualTo("Agent"));
            });
        }

        [Test]
        public async Task Delete_Should_Return_Correct_Redirection_On_Exception()
        {
            //Arrange
            var property = new Property() { Id = Guid.NewGuid() };
            agentPropService.Setup(aps => aps.DeleteAsync(property.Id))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Delete(property.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["FailedDelete"],
                    Is.EqualTo("Something went wrong, try again"));
            });

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo("Details"));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectResult!.RouteValues!["area"],
                    Is.EqualTo("Agent"));
                Assert.That(redirectResult!.RouteValues!["id"],
                    Is.EqualTo(property.Id));
            });
        }

        [Test]
        public async Task HttpGet_Edit_Should_Return_ViewResult_With_Correct_Model()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = new EditPropertyViewModel()
            {
                Address = "Test address",
                Description = "Test descrption",
                Name = "Test name",
                Price = 150,
                Size = 50,
                CountryId = 1,
                CityId = 1,
                BuildingTypeId = 1,
                PropertyStatusId = 1,
                PropertyTypeId = 1,
            };

            agentPropService.Setup(aps => aps.FindByIdAsync(id))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Edit(id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<EditPropertyViewModel>());

            var editModel = viewResult!.Model as EditPropertyViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(editModel!.CountryId, Is.EqualTo(model.CountryId));
                Assert.That(editModel.CityId, Is.EqualTo(model.CityId));
                Assert.That(editModel.Name, Is.EqualTo(model.Name));
                Assert.That(editModel.Address, Is.EqualTo(model.Address));
                Assert.That(editModel.Description, Is.EqualTo(model.Description));
                Assert.That(editModel.PropertyTypeId, Is.EqualTo(model.PropertyTypeId));
                Assert.That(editModel.PropertyStatusId, Is.EqualTo(model.PropertyStatusId));
                Assert.That(editModel.BuildingTypeId, Is.EqualTo(model.BuildingTypeId));
                Assert.That(editModel.Price, Is.EqualTo(model.Price));
                Assert.That(editModel.Size, Is.EqualTo(model.Size));
            });
        }

        [Test]
        public async Task HttpGet_Edit_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            var model = new EditPropertyViewModel();

            agentPropService.Setup(aps => aps.FindByIdAsync(id))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Edit(id);

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        [TestCase(null, null, 249, 39, null, null, null, null, null, null)]
        [TestCase(null, null, 100_000, 500, null, null, null, null, null, null)]
        [TestCase("Qwertyuio", "Qwertyuiopasdfghjklz", 249, 40, "Qwertyuiopasdfg", 1, 1, 1, 1, 1)]
        [TestCase(null, "Qwertyuiopasdfghjklz", 249, 40, "Qwertyuiopasd", 1, 1, 1, 1, 1)]
        public async Task HttpPost_Edit_Should_Return_Same_View_On_Invalid_Model(string name,
            string description, decimal price, int size, string address, int? countryId, int? cityId,
            int? propertyTypeId, int? propertyStatusId, int? buildingTypeId)
        {
            Guid id = Guid.NewGuid();
            var model = new EditPropertyViewModel()
            {
                Address = address,
                Description = description,
                Name = name,
                Price = price,
                Size = size,
                CountryId = countryId ?? 0,
                CityId = cityId ?? 0,
                BuildingTypeId = buildingTypeId ?? 0,
                PropertyStatusId = propertyStatusId ?? 0,
                PropertyTypeId = propertyTypeId ?? 0,
            };

            agentPropService.Setup(aps => aps.EditAsync(model, id, It.IsAny<ICollection<string>>(),
                It.IsAny<ICollection<CloudImage>>(), null!));

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
                buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
                agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description)
                || string.IsNullOrEmpty(address) || countryId == null
                || cityId == null || propertyTypeId == null || propertyStatusId == null
                || buildingTypeId == null)
            {
                controller.ModelState.AddModelError("Name", "All fields are required");
            }
            else if (price < 250 || price > 100000)
            {
                controller.ModelState.AddModelError("Price", "Price field must be between 250 and 100000");
            }
            else if (size < 40 || size > 500)
            {
                controller.ModelState.AddModelError("Size", "Size field must be between 40 and 500");
            }

            //Act
            var result = await controller.Edit(model, id);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task HttpPost_Edit_Should_Return_ViewResult_With_CorrectModel_On_Invalid_Dropdowns()
        {
            //Arrange
            var id = Guid.NewGuid();
            var model = new EditPropertyViewModel()
            {
                Address = "Test address",
                Description = "Test descrption",
                Name = "Test name",
                Price = 150,
                Size = 50,
                CountryId = 1,
                CityId = 1,
                BuildingTypeId = 1,
                PropertyStatusId = 1,
                PropertyTypeId = 1,
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            propertyTypeService.Setup(pts => pts.GetPropertyTypesAsync())
                .ReturnsAsync(new List<SelectPropertyTypeViewModel>());

            buildingTypeService.Setup(bts => bts.GetBuildingTypesAsync())
                .ReturnsAsync(new List<SelectBuildingTypeViewModel>());

            agentPropService.Setup(aps => aps.ExistByIdAsync<Country>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<City>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<PropertyType>(It.IsAny<int>()))
                .ReturnsAsync(false);

            agentPropService.Setup(aps => aps.ExistByIdAsync<BuildingType>(It.IsAny<int>()))
                .ReturnsAsync(false);

            var controller = new PropertyController(propertyTypeService.Object, countryService.Object,
               buildingTypeService.Object, cloudinaryService.Object, cloudinary.Object,
               agentPropService.Object, propertyStatusService.Object, repo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Edit(model, id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(controller.TempData["InvalidDropdownOption"],
                     Is.EqualTo("You must choose a valid option from the dropdowns"));
            });
        }

        private static void AssertForTempDataViewMethod(PropertyController propertyController,
            IActionResult result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(propertyController.TempData["UnexpectedError"],
                    Is.EqualTo("Something went wrong, please try again"));
            });
        }

        private static HttpContext CreateHttpContext(ClaimsPrincipal userPrincipal)
        {
            var context = new DefaultHttpContext
            {
                User = userPrincipal
            };

            return context;
        }
    }
}
