namespace HomeXplorer.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using HomeXplorer.ViewModels;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property;

    using static HomeXplorer.Common.UserRoleConstants;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRenterPropertyService renterPropertyService;
        private readonly IReviewService reviewService;

        public HomeController(ILogger<HomeController> logger,
            IRenterPropertyService renterPropertyService,
            IReviewService reviewService)
        {
            _logger = logger;
            this.renterPropertyService = renterPropertyService;
            this.reviewService = reviewService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            bool hasLoggedInUser = this.User?.Identity?.IsAuthenticated ?? false;

            if (hasLoggedInUser && this.User!.IsInRole(Agent))
            {
                return this.RedirectToAction("Index", "Home", new { area = Agent });
            }
            else if (hasLoggedInUser && this.User!.IsInRole(Renter))
            {
                return this.RedirectToAction("Index", "Home", new { area = Renter });
            }
            else if (hasLoggedInUser && this.User!.IsInRole(Administrator))
            {
                return this.RedirectToAction("Index", "Dashboard", new { area = Administrator });
            }

            var slider = await this.renterPropertyService.GetLastThreeAddedForSliderAsync();
            var latestProperties = await this.renterPropertyService.GetLastThreeAddedPropertiesAsync();
            var approvedReviews = await this.reviewService.GetAllReviewsAsync();

            var model = new BaseMainPageViewModel()
            {
                SliderProperties = slider,
                LatestProperties = latestProperties,
                ApprovedReviews = approvedReviews
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int error)
        {
            if (error == 404)
            {
                return View("NotFound");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}