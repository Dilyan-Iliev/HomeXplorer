namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;

    public class HomeController : BaseRenterController
    {
        private readonly IRenterPropertyService renterPropertyService;
        private readonly IReviewService reviewService;

        public HomeController(IRenterPropertyService renterPropertyService,
            IReviewService reviewService)
        {
            this.renterPropertyService = renterPropertyService;
            this.reviewService = reviewService;
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

            var approvedReviews = await this.reviewService.GetAllReviewsAsync();

            var model = new MainPageViewModel()
            {
                SliderProperties = slider,
                LastThreePropertiesNearby = nearbys,
                LatestProperties = latest,
                ApprovedReviews = approvedReviews
            };

            return View(model);
        }
    }
}
