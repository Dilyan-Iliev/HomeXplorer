namespace HomeXplorer.Tests.AdminTests
{
    using HomeXplorer.Areas.Administrator.Controllers;
    using HomeXplorer.Data.Entities;
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
            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

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

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

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

        [Test]
        public async Task HttpPost_AddCountry_Should_Return_TempDataView_Method_On_Exception()
        {
            // Arrange
            var model = new AddNonExistingCountryViewModel
            {
                Name = "TestCountry"
            };

            adminService.Setup(a => a.AddNewCountryAsync(model))
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            // Act
            var result = await adminController.AddCountry(model);

            // Assert
            Assert.That(result, Is.Not.Null);

            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public async Task AllCities_Should_Return_Correct_Data()
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

            adminService.Setup(a => a.GetAllCitiesFromCountryAsync())
                .ReturnsAsync(expectedModel);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = await adminController.AllCities();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult?.Model, Is.EqualTo(expectedModel));
        }

        [Test]
        public async Task AllCities_Should_Return_TempDataView_Method_On_Exception()
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

            adminService.Setup(a => a.GetAllCitiesFromCountryAsync())
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AllCities();

            //Assert
            Assert.That(result, Is.Not.Null);

            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public void HttpGet_AddCity_Should_Return_Correct_Data()
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

            adminService.Setup(a => a.GetAllCitiesFromCountryAsync())
                .ReturnsAsync(expectedModel);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

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
        public async Task HttpPost_AddCity_Should_Add_New_NonExisting_City()
        {
            //Arrange
            var model = new AddNonExistingCityToExistingCountryViewModel
            {
                CityName = "TestName",
                CountryId = 1
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            agentPropertyService.Setup(aps => aps.ExistByIdAsync<Country>(model.CountryId))
                .ReturnsAsync(true);

            adminService.Setup(a => a.AddNewCityAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddCity(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectToActionResult!.ActionName, Is.EqualTo(nameof(adminController.AllCities)));
                Assert.That(redirectToActionResult!.ControllerName, Is.EqualTo("Dashboard"));
                Assert.That(redirectToActionResult!.RouteValues!["area"], Is.EqualTo("Administrator"));
            });

            Assert.That(adminController.TempData["CitySuccessfullyAdded"],
                Is.EqualTo("The city was successfully added"));
        }

        [Test]
        public async Task HttpPost_AddCity_Should_Not_Add_New_NonExisting_City_If_Country_Does_Not_Exist()
        {
            //Arrange
            var model = new AddNonExistingCityToExistingCountryViewModel
            {
                CityName = "TestName",
                CountryId = 1
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            agentPropertyService.Setup(aps => aps.ExistByIdAsync<Country>(model.CountryId))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddCity(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.Multiple(() =>
            {
                Assert.That(viewResult!.Model, Is.Not.Null);
                Assert.That(viewResult.Model, Is.TypeOf<AddNonExistingCityToExistingCountryViewModel>());
            });

            Assert.That(adminController.TempData["InvalidDropdownOption"], Is.EqualTo("You must choose a valid option from the dropdowns"));
        }

        [Test]
        [TestCase(null, 1)]
        [TestCase(null, null)]
        [TestCase("CityName", null)]
        [TestCase("T", 1)]
        [TestCase("jPh9kYVzAxFknu8EdIge04JR5TfRrA2GuG4H7yF2eaWUZvi6zrmk3q3qZEXuSgswvb4k0VmOQtGwwdsZ4D", 1)]
        public async Task HttpPost_AddCity_On_Invalid_Model_Should_Return_Same_View_With_Model(string modelName,
            int? countryId)
        {
            //Arrange
            var model = new AddNonExistingCityToExistingCountryViewModel
            {
                CityName = modelName,
                CountryId = countryId ?? 0
            };

            adminService.Setup(a => a.AddNewCityAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            if (string.IsNullOrEmpty(modelName) || countryId == null)
            {
                adminController.ModelState.AddModelError("Name", "All fields are required");
            }
            else
            {
                adminController.ModelState.AddModelError("Name", "The Name field must be between 2 and 85 characters long.");
            }

            //Act
            var result = await adminController.AddCity(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task HttpPost_Add_City_Should_Return_TempDataView_Method_On_Exception()
        {
            //Arrange
            var model = new AddNonExistingCityToExistingCountryViewModel
            {
                CityName = "TestName",
                CountryId = 1
            };

            countryService.Setup(cs => cs.GetCountriesAsync())
                .ReturnsAsync(new List<SelectCountryViewModel>());

            agentPropertyService.Setup(aps => aps.ExistByIdAsync<Country>(model.CountryId))
                .ReturnsAsync(true);

            adminService.Setup(a => a.AddNewCityAsync(model))
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddCity(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public async Task AllPropertyTypes_Should_Return_Correct_Data()
        {
            //Arrange
            var expectedData = new List<string>() { "Test1", "Test2" };

            adminService.Setup(a => a.GetAllPropertyTypesAsync())
                .ReturnsAsync(expectedData);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = await adminController.AllPropertyTypes();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.EqualTo(expectedData));
        }

        [Test]
        public async Task AllPropertyTypes_Should_Return_TempDataView_Method_On_Exception()
        {
            //Arrange
            var expectedData = new List<string>() { "Test1", "Test2" };

            adminService.Setup(a => a.GetAllPropertyTypesAsync())
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AllPropertyTypes();

            //Assert
            Assert.That(result, Is.Not.Null);
            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public void HttpGet_AddPropertyType_Should_Return_ViewResult()
        {
            //Arrange
            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = adminController.AddPropertyType();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AddNonExistingPropertyTypeViewModel>());
        }

        [Test]
        public async Task HttpPost_AddPropertyType_Should_Add_New_NonExisting_PropertyType()
        {
            //Arrange
            var model = new AddNonExistingPropertyTypeViewModel() { Name = "Test" };

            adminService.Setup(a => a.AddNewPropertyTypeAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddPropertyType(model);

            //Arrange
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectResult!.ActionName, Is.EqualTo(nameof(adminController.AllPropertyTypes)));
                Assert.That(redirectResult!.ControllerName, Is.EqualTo("Dashboard"));
                Assert.That(redirectResult!.RouteValues!["area"], Is.EqualTo("Administrator"));
            });

            Assert.That(adminController.TempData["PropertyTypeSuccessfullyAdded"],
                Is.EqualTo("The property type was successfully added"));
        }

        [Test]
        public async Task HttpPost_AddPropertyType_Should_Not_Add_Existing_PropertyType()
        {
            //Arrange
            var model = new AddNonExistingPropertyTypeViewModel() { Name = "Test" };

            adminService.Setup(a => a.AddNewPropertyTypeAsync(model))
                .ReturnsAsync(true);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddPropertyType(model);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(adminController.TempData["InvalidPropertyTypeAdded"],
                    Is.EqualTo("This property type already exists"));
            });
        }

        [Test]
        [TestCase(null)]
        [TestCase("Asd")]
        [TestCase("Qwertyuiopasdfgh")]
        public async Task HttpPost_AddPropertyType_On_Invalid_Model_Should_Return_Same_View_With_Model(string modelName)
        {
            //Arrange
            var model = new AddNonExistingPropertyTypeViewModel() { Name = modelName };

            adminService.Setup(a => a.AddNewPropertyTypeAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            if (string.IsNullOrEmpty(modelName))
            {
                adminController.ModelState.AddModelError("Name", "All fields are required");
            }
            else
            {
                adminController.ModelState.AddModelError("Name", "The field must be between 4 and 15 characters long.");
            }

            //Act
            var result = await adminController.AddPropertyType(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task HttpPost_AddPropertyType_Should_Return_TempDataView_On_Exception()
        {
            //Arrange
            var model = new AddNonExistingPropertyTypeViewModel() { Name = "Test" };

            adminService.Setup(a => a.AddNewPropertyTypeAsync(model))
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddPropertyType(model);

            //Assert
            Assert.That(result, Is.Not.Null);
            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public async Task AllBuildingTypes_Should_Return_Correct_Data()
        {
            //Arrange
            var expectedResult = new List<string> { "Test1", "Test2" };

            adminService.Setup(a => a.GetAllBuildingTypesAsync())
                .ReturnsAsync(expectedResult);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = await adminController.AllBuildingTypes();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task AllBuildingTypes_Should_Return_TempDataView_Method_On_Excception()
        {
            //Arrange
            var expectedResult = new List<string> { "Test1", "Test2" };

            adminService.Setup(a => a.GetAllBuildingTypesAsync())
                .ThrowsAsync(new Exception());

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AllBuildingTypes();

            //Assert
            Assert.That(result, Is.Not.Null);
            AssertForTempDataViewMethod(adminController, result);
        }

        [Test]
        public void HttpGet_Add_Building_Type_Should_Return_ViewResult()
        {
            //Arrange
            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            //Act
            var result = adminController.AddBuildingType();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.Not.Null);
            Assert.That(viewResult!.Model, Is.TypeOf<AddNonExistingBuildingTypeViewModel>());
        }

        [Test]
        public async Task HttpPost_AddBuildingType_Should_Add_NonExisting_BuildingType()
        {
            //Arrange
            var model = new AddNonExistingBuildingTypeViewModel() { Name = "Test" };

            adminService.Setup(a => a.AddNewBuildingTypeAsync(model))
                .ReturnsAsync(false);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddBuildingType(model);

            //Assert
            adminService.Verify(x => x.AddNewBuildingTypeAsync(model), Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectToActionResult!.ActionName, Is.EqualTo(nameof(adminController.AllBuildingTypes)));
                Assert.That(redirectToActionResult!.ControllerName, Is.EqualTo("Dashboard"));
                Assert.That(redirectToActionResult!.RouteValues!["area"], Is.EqualTo("Administrator"));
            });

            Assert.That(adminController.TempData["BuildingTypeSuccessfullyAdded"],
                Is.EqualTo("The building type was successfully added"));
        }

        [Test]
        public async Task HttpPost_AddBuildingType_Should_Not_Add_Existing_BuildingType()
        {
            //Arrange
            var model = new AddNonExistingBuildingTypeViewModel() { Name = "Test" };

            adminService.Setup(a => a.AddNewBuildingTypeAsync(model))
                .ReturnsAsync(true);

            var adminController =
                new DashboardController(adminService.Object, countryService.Object, agentPropertyService.Object);

            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            adminController.TempData = tempData;

            //Act
            var result = await adminController.AddBuildingType(model);

            //Assert
            adminService.Verify(x => x.AddNewBuildingTypeAsync(model), Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ViewResult>());
                Assert.That(adminController.TempData["InvalidBuildingTypeAdded"],
                    Is.EqualTo("This building type already exists"));
            });
        }

        private static void AssertForTempDataViewMethod(DashboardController adminController, IActionResult result)
        {
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectionResult = result as RedirectToActionResult;
            Assert.Multiple(() =>
            {
                Assert.That(redirectionResult!.ActionName, Is.EqualTo(nameof(adminController.Index)));
                Assert.That(redirectionResult!.ControllerName, Is.EqualTo("Dashboard"));
                Assert.That(redirectionResult!.RouteValues!["area"], Is.EqualTo("Administrator"));
            });

            Assert.That(adminController.TempData["DashboardError"],
                Is.EqualTo("Something went wrong, please try again"));
        }
    }
}
