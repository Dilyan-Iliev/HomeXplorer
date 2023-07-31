﻿namespace HomeXplorer.Tests.AdminTests
{
    using HomeXplorer.Areas.Administrator.Controllers;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;

    public class AdminDashboardControllerTests
    {
        private Mock<IAdminService> adminService;
        private Mock<ICountryService> countryService;
        private Mock<IAgentPropertyService> agentPropertyService;

        [SetUp]
        public void SetUp()
        {
            adminService = new Mock<IAdminService>();
            countryService = new Mock<ICountryService>();
            agentPropertyService = new Mock<IAgentPropertyService>();
        }

        [Test]
        public async Task Index_Should_Return_Correct_Data()
        {
            var expectedDashboardStatistics = new DashboardViewModel()
            {
                TotalPropertiesUploaded = 10,
                TotalReviewsAdded = 1,
                TotalLikesOfProperties = 5,
                TotalRentedProperties = 3,
                Reviews = new List<DashboardReviewViewModel>
                {
                    new DashboardReviewViewModel
                    {
                        AddedOn = "01/01/2023",
                        Description = "Review 1...",
                        ReviewCreatorName = "John Doe",
                        ReviewCreatorAvatarUrl = "url1",
                        IsApproved = true
                    }
                }
            };

            adminService.Setup(a => a.GetDashboardInfoAsync())
                .ReturnsAsync(expectedDashboardStatistics);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = await adminController.Index();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(expectedDashboardStatistics, Is.EqualTo(viewResult!.Model));
        }

        [Test]
        public async Task All_Countries_Should_Return_Correct_Data_When_No_Exceptions_Occure()
        {
            //Arrange
            var expectedModel = new AllCountriesWithCitiesViewModel()
            {
                Countries = new List<SelectCountryViewModel>()
                {
                    new SelectCountryViewModel() { Id = 1, Name = "TestCountry"}
                },
                Cities = new List<SelectCityViewModel>()
                {
                    new SelectCityViewModel() { Id = 1, Name = "TestCity1"},
                    new SelectCityViewModel() { Id = 2, Name = "TestCity2"},
                }
            };

            adminService.Setup(a => a.GetAllCountriesAsync())
                .ReturnsAsync(expectedModel);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = await adminController.AllCountries();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(expectedModel, Is.EqualTo(viewResult!.Model));

            var model = viewResult.Model as AllCountriesWithCitiesViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(model!.Countries, Is.Not.Null);
                Assert.That(model.Countries.Count(), Is.GreaterThan(0));
                Assert.That(model.Countries.Count(), Is.EqualTo(1));

                Assert.That(model.Cities, Is.Not.Null);
                Assert.That(model.Cities.Count(), Is.GreaterThan(0));
                Assert.That(model.Cities.Count(), Is.EqualTo(2));
            });
        }

        [Test]
        public void HttpGet_Add_Country_Should_Return_ViewResult()
        {
            //Arrange
            var adminController = new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = adminController.AddCountry();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.Multiple(() =>
            {
                Assert.That(viewResult!.Model, Is.Not.Null);
                Assert.That(viewResult.Model, Is.TypeOf<AddNonExistingCountryViewModel>());
            });
        }

        [Test]
        public async Task HttpPost_Add_Country_Should_Add_New_Country_If_Country_Does_Not_Exist()
        {
            //Arrange
            var model = new AddNonExistingCountryViewModel()
            {
                Name = "TestName"
            };

            //country does not exist
            adminService.Setup(a => a.AddNewCountryAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddCountry(model);

            //Assert
            adminService.Verify(a => a.AddNewCountryAsync(model), Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo(nameof(adminController.AllCountries)));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Dashboard"));
                Assert.That(redirectResult!.RouteValues!["area"], Is.EqualTo("Administrator"));
            });

            Assert.That(adminController.TempData["CountrySuccessfullyAdded"],
                Is.EqualTo("The country was successfully added"));
        }

        [Test]
        public async Task HttpPost_Add_Country_Should_Not_Add_Existing_Country()
        {
            //Arrange
            var model = new AddNonExistingCountryViewModel()
            {
                Name = "TestName"
            };

            adminService.Setup(a => a.AddNewCountryAsync(model))
                .ReturnsAsync(true);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddCountry(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(adminController.TempData["InvalidCountryAdded"],
                    Is.EqualTo("This country already exists"));
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("T")]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")]
        public async Task HttpPost_AddCountry_On_Invalid_Model_Should_Return_Same_View_With_Model(string modelName)
        {
            //Arrange
            var model = new AddNonExistingCountryViewModel() { Name = modelName! };

            adminService.Setup(a => a.AddNewCountryAsync(model))
                .ReturnsAsync(false);

            var adminController = new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            if (string.IsNullOrEmpty(modelName))
            {
                adminController.ModelState.AddModelError("Name", "The Name field is required.");
            }
            else
            {
                adminController.ModelState.AddModelError("Name", "The Name field must be between 4 and 56 characters long.");
            }

            //Act
            var result = await adminController.AddCountry(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
