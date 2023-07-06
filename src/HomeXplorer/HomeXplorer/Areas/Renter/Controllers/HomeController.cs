namespace HomeXplorer.Areas.Renter.Controllers
{
    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseRenterController
    {
        private readonly IRenterPropertyService renterPropertyService;

        public HomeController(IRenterPropertyService renterPropertyService)
        {
            this.renterPropertyService = renterPropertyService;
        }

        public async Task<IActionResult> Index()
        {
            string? userId = this.User?.GetId();

            IEnumerable<LatestPropertiesViewModel> latest =
                await this.renterPropertyService.GetLastThreeAddedPropertiesAsync();

            IEnumerable<LatestPropertiesViewModel> nearbys = null!;

            if (userId != null)
            {
                nearbys = await this.renterPropertyService.GetLastThreePropertiesNearbyAsync(userId);    
            }

            var slider = await this.renterPropertyService.GetLastThreeAddedForSliderAsync();

            var model = new MainPageViewModel()
            {
                SliderProperties = slider,
                LastThreePropertiesNearby = nearbys,
                LatestProperties = latest
            };

            return View(model);
        }
    }
}
