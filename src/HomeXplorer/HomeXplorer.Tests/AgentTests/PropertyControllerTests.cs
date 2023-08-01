namespace HomeXplorer.Tests.AgentTests
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    using Moq;
    using CloudinaryDotNet;

    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Areas.Agent.Controllers;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.ViewModels.Property.Enums;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using HomeXplorer.ViewModels.Country;
    using HomeXplorer.ViewModels.PropertyType;
    using HomeXplorer.ViewModels.BuildingType;

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
