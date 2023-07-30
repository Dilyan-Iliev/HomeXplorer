namespace HomeXplorer.Tests.CityTests
{
    using HomeXplorer.Controllers;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.City;
    using Microsoft.AspNetCore.Mvc;
    using Moq;

    public class CityControllerTests
    {
        [Test]
        public async Task City_Based_On_Country_Should_Return_Correct_Json_With_Cities()
        {
            //Arrange
            int countryId = 1;
            var cities = new List<SelectCityViewModel>
            {
                new SelectCityViewModel {Id = 1, Name = "City1"},
                new SelectCityViewModel {Id = 2, Name = "City2"},
            };

            var mockCityService = new Mock<ICityService>();
            mockCityService.Setup(c => c.GetAllCitiesByCountryIdAsync(countryId))
                .ReturnsAsync(cities);

            var controller = new CityController(mockCityService.Object);

            //Act
            var result = await controller.CityBasedOnCountry(countryId);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<JsonResult>());

            var jsonResult = result as JsonResult;
            Assert.That(jsonResult?.Value, Is.Not.Null);

            var resultCities = jsonResult.Value as List<SelectCityViewModel>;
            Assert.That(resultCities, Is.Not.Null);
            Assert.That(resultCities, Has.Count.EqualTo(cities.Count));
        }

        [Test]
        public async Task City_Based_On_Country_Should_Return_BadRequest_On_Exception()
        {
            //Arrange
            int countryId = 1;
            var mockedCityService = new Mock<ICityService>();

            mockedCityService.Setup(c => c.GetAllCitiesByCountryIdAsync(countryId))
                 .ThrowsAsync(new Exception());

            var controller = new CityController(mockedCityService.Object);

            //Act
            var result = await controller.CityBasedOnCountry(countryId);

            //Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}
