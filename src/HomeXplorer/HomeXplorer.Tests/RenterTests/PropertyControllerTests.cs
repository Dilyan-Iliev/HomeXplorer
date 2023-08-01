namespace HomeXplorer.Tests.RenterTests
{
    using System.Security.Claims;

    using Moq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Areas.Renter.Controllers;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Renter;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    public class PropertyControllerTests
    {
        private Mock<IRenterPropertyService> propertyService;
        private ClaimsPrincipal userPrincipal;
        private HttpContext context;

        [SetUp]
        public void SetUp()
        {
            propertyService = new Mock<IRenterPropertyService>();

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
        public async Task AllProperties_Should_Return_ViewResult_With_Model()
        {
            //Arrange
            var model = new RenterAllPropertiesViewModel()
            {
                PageNumber = 1,
                PageSize = 6,
                PropertySorting = PropertySorting.Default,
                TotalPages = 3,
            };

            propertyService.Setup(ps => ps.AllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PropertySorting>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.AllProperties();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<RenterAllPropertiesViewModel>());
        }

        [Test]
        public async Task AllProperties_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new RenterAllPropertiesViewModel()
            {
                PageNumber = 1,
                PageSize = 6,
                PropertySorting = PropertySorting.Default,
                TotalPages = 3,
            };

            propertyService.Setup(ps => ps.AllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<PropertySorting>()))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.AllProperties();

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        public async Task AllNearby_Should_Return_ViewResult_With_Model()
        {
            //Arrange
            var model = new RenterAllPropertiesViewModel()
            {
                PageNumber = 1,
                PageSize = 6,
                PropertySorting = PropertySorting.Default,
                TotalPages = 3,
            };

            propertyService.Setup(ps => ps.AllNearbyAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<PropertySorting>(), It.IsAny<string>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.AllNearby();

            //Assert
            propertyService.Verify(ps => ps.AllNearbyAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<PropertySorting>(), It.IsAny<string>()), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<RenterAllPropertiesViewModel>());
        }

        [Test]
        public async Task AllNearby_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new RenterAllPropertiesViewModel()
            {
                PageNumber = 1,
                PageSize = 6,
                PropertySorting = PropertySorting.Default,
                TotalPages = 3,
            };

            propertyService.Setup(ps => ps.AllNearbyAsync(It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<PropertySorting>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.AllNearby();

            //Assert
            AssertForTempDataViewMethod(controller, result);
        }

        [Test]
        public async Task Details_Should_Return_ViewResult_On_Found_Property()
        {
            var model = new RenterDetailsPropertyViewModel()
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
                IsAddedToFavs = true,
                IsRented = false,
                AgentEmail = "Test Mail",
                AgentPhone = "Test Phone",
                AgentFullName = "Test Agent Name",
                AgentProfilePicture = "Test Picture",
                Images = null!
            };

            propertyService.Setup(ps => ps.GetPropertyDetailsAsync(It.IsAny<Guid>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            //Act
            var result = await controller.Details(model.Id);

            //Assert
            propertyService.Verify(ps => ps.GetPropertyDetailsAsync(It.IsAny<Guid>(),
                It.IsAny<string>()), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<RenterDetailsPropertyViewModel>());

            var detailsModel = viewResult.Model as RenterDetailsPropertyViewModel;
            Assert.Multiple(() =>
            {
                Assert.That(detailsModel!.IsRented, Is.False);
                Assert.That(detailsModel.IsAddedToFavs, Is.True);
                Assert.That(detailsModel.Id, Is.EqualTo(model.Id));
                Assert.That(detailsModel.Name, Is.EqualTo(model.Name));
                Assert.That(detailsModel.Size, Is.EqualTo(model.Size));
                Assert.That(detailsModel.City, Is.EqualTo(model.City));
                Assert.That(detailsModel.Price, Is.EqualTo(model.Price));
                Assert.That(detailsModel.Images, Is.EqualTo(model.Images));
                Assert.That(detailsModel.Country, Is.EqualTo(model.Country));
                Assert.That(detailsModel.AddedOd, Is.EqualTo(model.AddedOd));
                Assert.That(detailsModel.Address, Is.EqualTo(model.Address));
                Assert.That(detailsModel.AgentPhone, Is.EqualTo(model.AgentPhone));
                Assert.That(detailsModel.AgentEmail, Is.EqualTo(model.AgentEmail));
                Assert.That(detailsModel.Description, Is.EqualTo(model.Description));
                Assert.That(detailsModel.AgentFullName, Is.EqualTo(model.AgentFullName));
                Assert.That(detailsModel.AgentProfilePicture, Is.EqualTo(model.AgentProfilePicture));
            });
        }

        [Test]
        public async Task Details_Should_Return_Correct_Redirection_When_Property_Is_Null()
        {
            RenterDetailsPropertyViewModel model = null!;

            propertyService.Setup(ps => ps.GetPropertyDetailsAsync(It.IsAny<Guid>(),
                It.IsAny<string>()))
                .ReturnsAsync(model);

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Details(Guid.NewGuid());

            //Assert
            propertyService.Verify(ps => ps.GetPropertyDetailsAsync(It.IsAny<Guid>(),
                It.IsAny<string>()), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<RedirectToActionResult>());
                Assert.That(controller.TempData["DetailsError"],
                    Is.EqualTo("Can't show the details of the property"));
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
        public async Task Details_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new RenterDetailsPropertyViewModel();

            propertyService.Setup(ps => ps.GetPropertyDetailsAsync(It.IsAny<Guid>(),
                It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            var controller = new PropertyController(propertyService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = context
                }
            };

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            //Act
            var result = await controller.Details(Guid.NewGuid());

            //Assert
            AssertForTempDataViewMethod(controller, result);
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
