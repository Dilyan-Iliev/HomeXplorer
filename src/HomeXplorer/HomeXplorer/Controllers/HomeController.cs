namespace HomeXplorer.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.ViewModels;
    using Microsoft.AspNetCore.Authorization;

    using static HomeXplorer.Common.UserRoleConstants;
    using HomeXplorer.Services.Contracts;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRenterPropertyService renterPropertyService;

        public HomeController(ILogger<HomeController> logger,
            IRenterPropertyService renterPropertyService)
        {
            _logger = logger;
            this.renterPropertyService = renterPropertyService;
        }

        [AllowAnonymous]
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

            var model = await this.renterPropertyService.GetLastThreeAddedForSliderAsync();
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
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