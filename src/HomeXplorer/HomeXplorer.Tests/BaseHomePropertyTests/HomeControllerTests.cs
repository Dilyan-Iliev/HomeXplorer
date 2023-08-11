namespace HomeXplorer.Tests.BaseHomePropertyTests
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;

    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;

    public class HomeControllerTests
    {
        private Mock<ILogger<Controllers.HomeController>> logger;
        private Mock<IRenterPropertyService> rps;
        private Mock<IReviewService> rs;
        private Mock<IHomePropertyService> hps;

        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger<Controllers.HomeController>>();
            rps = new Mock<IRenterPropertyService>();
            rs = new Mock<IReviewService>();
            hps = new Mock<IHomePropertyService>();
        }

        [Test]
        public async Task All_Method_Should_Return_ViewResult_With_Correct_Model()
        {
            //Arrange
            var model = new List<LatestPropertiesViewModel>()
            {
                new LatestPropertiesViewModel()
                {
                    Id = Guid.NewGuid(),
                    City = "Test",
                    CoverImageUrl = "Test",
                    Name = "Test",
                    Price = 15,
                    Size = 20,
                    Status = "Test",
                    Visits = 1,
                    AddedOn = DateTime.UtcNow.ToString("MM/dd/yyyy")
                }
            };

            hps.Setup(x => x.GetAllPropertiesAsync())
                .ReturnsAsync(model);

            var controller = new Controllers.HomeController(
                logger.Object,
                rps.Object,
                rs.Object,
                hps.Object);

            //Act
            var result = await controller.All();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.Multiple(() =>
            {
                Assert.That(viewResult!.Model, Is.Not.Null);
                Assert.That(viewResult.Model, Is.TypeOf<List<LatestPropertiesViewModel>>());
            });

            var modelResult = viewResult!.Model as List<LatestPropertiesViewModel>;
            Assert.That(modelResult!, Is.Not.Empty);
            Assert.Multiple(() =>
            {
                Assert.That(modelResult![0].Id, Is.EqualTo(model[0].Id));
                Assert.That(modelResult[0].City, Is.EqualTo(model[0].City));
                Assert.That(modelResult[0].Size, Is.EqualTo(model[0].Size));
                Assert.That(modelResult[0].Price, Is.EqualTo(model[0].Price));
                Assert.That(modelResult[0].AddedOn, Is.EqualTo(model[0].AddedOn));
                Assert.That(modelResult[0].Visits, Is.EqualTo(model[0].Visits));
                Assert.That(modelResult[0].CoverImageUrl, Is.EqualTo(model[0].CoverImageUrl));
                Assert.That(modelResult[0].Status, Is.EqualTo(model[0].Status));
            });
        }

        [Test]
        public async Task All_Method_Should_Return_Correct_Redirection_On_Exception()
        {
            //Arrange
            hps.Setup(x => x.GetAllPropertiesAsync())
                .ThrowsAsync(new Exception());

            var controller = new Controllers.HomeController(
                logger.Object,
                rps.Object,
                rs.Object,
                hps.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.All();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["UnexpectedError"],
                    Is.EqualTo("Something went wrong, please try again"));
            });

            var redirectionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectionResult!.ActionName, Is.EqualTo("Index"));
                Assert.That(redirectionResult.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectionResult.RouteValues, Is.Null);
            });
        }
    }
}
