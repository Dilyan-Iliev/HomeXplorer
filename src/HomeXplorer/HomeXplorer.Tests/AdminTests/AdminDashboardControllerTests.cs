namespace HomeXplorer.Tests.AdminTests
{
    using HomeXplorer.Areas.Administrator.Controllers;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Admin;
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
    }
}
