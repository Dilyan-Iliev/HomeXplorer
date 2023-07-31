namespace HomeXplorer.Tests.AdminTests
{
    using HomeXplorer.Areas.Administrator.Controllers;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.ViewModels.City;
    using HomeXplorer.ViewModels.Country;
    using Microsoft.AspNetCore.Mvc;
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

        //[Test]
        //public async Task Index_Returns_TempDataView_Method_On_Exception()
        //{
        //    adminService.Setup(a => a.GetDashboardInfoAsync())
        //.ThrowsAsync(new Exception("Simulated exception"));

        //    var adminController =
        //        new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

        //    // Act
        //    var result = await adminController.Index();

        //    // Assert
        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result, Is.TypeOf<ViewResult>());

        //    var viewResult = result as ViewResult;
        //    Assert.AreEqual("TempDataView", viewResult.ViewName);
        //}

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
    }
}
