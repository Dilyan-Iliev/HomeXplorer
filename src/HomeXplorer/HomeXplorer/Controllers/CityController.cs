namespace HomeXplorer.Controllers
{
    using HomeXplorer.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
