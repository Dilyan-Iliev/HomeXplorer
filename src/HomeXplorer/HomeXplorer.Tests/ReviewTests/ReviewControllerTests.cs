namespace HomeXplorer.Tests.ReviewTests
{
    using Moq;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Areas.Renter.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using HomeXplorer.ViewModels.Property.Renter;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ReviewControllerTests
    {
        private Mock<IReviewService> reviewService;
        private ClaimsPrincipal userPrincipal;
        private HttpContext context;

        [SetUp]
        public void SetUp()
        {
            reviewService = new Mock<IReviewService>();

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
        public void HtpGet_AddReview_Should_Return_ViewResult()
        {
            //Arrange
            var controller = new ReviewController(reviewService.Object);

            //Act
            var result = controller.AddReview();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AddReviewViewModel>());
        }

        [Test]
        public async Task HttpPost_AddReview_Should_Successfully_Redirect_After_Adding_Review()
        {
            //Arrange
            reviewService
                .Setup(service => service.AddAsync(It.IsAny<AddReviewViewModel>(), It.IsAny<string>()))
                .Verifiable();

            // Create the controller and set the HttpContext
            var controller = new ReviewController(reviewService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var model = new AddReviewViewModel
            {
                Description = "Test review description",
                FullName = "Test Name"
            };

            // Act
            var result = await controller.AddReview(model);

            // Assert
            reviewService.Verify(service => service.AddAsync(model, It.IsAny<string>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["ReviewApprove"],
                    Is.EqualTo("Your review awaits approve from administrator"));
            });

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo("Index"));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Home"));
                Assert.That(redirectResult!.RouteValues!["area"], Is.EqualTo("Renter"));
            });
        }

        [Test]
        public async Task HttpPost_AddReview_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            reviewService
                .Setup(service => service.AddAsync(It.IsAny<AddReviewViewModel>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new ReviewController(reviewService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            var model = new AddReviewViewModel
            {
                Description = "Test review description",
                FullName = "Test Name"
            };

            // Act
            var result = await controller.AddReview(model);

            //Assert
            reviewService.Verify(rs => rs.AddAsync(model, It.IsAny<string>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(controller.TempData["ReviewError"],
                    Is.EqualTo("Something went wrong, please try again"));
            });

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AddReviewViewModel>());
        }

        [Test]
        [TestCase("Name", null)]
        [TestCase(null, null)]
        [TestCase("Name", "Qwertyuiopasdf")]
        [TestCase("Name", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        public async Task HttpPost_AddReview_On_Invalid_Model_Should_Return_Same_View_With_Model(string name,
            string description)
        {
            //Arrange
            var model = new AddReviewViewModel() { FullName = name, Description = description };

            reviewService.Setup(rs => rs.AddAsync(model, It.IsAny<string>()))
                .Verifiable();

            var controller = new ReviewController(reviewService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            if (string.IsNullOrEmpty(description))
            {
                controller.ModelState.AddModelError("Description", "The Description field is required.");
            }
            else
            {
                controller.ModelState.AddModelError("Description", "The Description field must be between 15 and 120 characters long.");
            }

            //Act
            var result = await controller.AddReview(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
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
