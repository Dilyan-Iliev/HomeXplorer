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
        private readonly IHomePropertyService homePropertyService;

        public HomeController(ILogger<HomeController> logger,
            IRenterPropertyService renterPropertyService,
            IReviewService reviewService,
            IHomePropertyService homePropertyService)
        {
            _logger = logger;
            this.renterPropertyService = renterPropertyService;
            this.reviewService = reviewService;
            this.homePropertyService = homePropertyService;
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

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                var model = await homePropertyService.GetAllPropertiesAsync();
                return this.View(model);
            }
            catch (Exception)
            {
                this.TempData["UnexpectedError"] = "Something went wrong, please try again";
                return this.RedirectToAction(nameof(Index));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int error)
        {
            if (error == 404)
            {
                return this.View("NotFound");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}