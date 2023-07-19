namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;

    public class DashboardController : BaseAdminController
    {
        private readonly IAdminService adminService;

        public DashboardController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                DashboardViewModel dashboardStatistics = await this.adminService.GetDashboardInfoAsync();
                return this.View(dashboardStatistics);

            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllCountries()
        {
            var model = await this.adminService.GetAllCountriesAsync();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult AddCountry()
        {
            return this.View(new AddNonExistingCountryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(AddNonExistingCountryViewModel country)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(country);
            }

            try
            {
                bool countryExists = await this.adminService.AddNewCountryAsync(country);

                if (countryExists)
                {
                    this.TempData["InvalidCountryAdded"] = "This country already exists";
                    return this.View();
                }

                this.TempData["CountrySuccessfullyAdded"] = "The country was successfully added";
                //Add this tempdata to the AllCountriesView
                return this.RedirectToAction(nameof(AllCountries), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        private IActionResult TempDataView()
        {
            this.TempData["DashboardError"] = "Something went wrong, please try again";
            return this.View();
        }
    }
}
