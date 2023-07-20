namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HomeXplorer.Services.Contracts;

    public class CityController : BaseController
    {
        private readonly ICityService cityService;

        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> CityBasedOnCountry(int countryId)
        {
            var cities = await this.cityService.GetAllCitiesByCountryIdAsync(countryId);

            return this.Json(cities);
        }
    }
}
