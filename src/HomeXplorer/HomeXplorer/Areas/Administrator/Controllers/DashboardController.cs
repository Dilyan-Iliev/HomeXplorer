namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;
    using HomeXplorer.Data.Entities;

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
            try
            {
                var model = await this.adminService.GetAllCountriesAsync();
                return this.View(model);

            }
            catch (Exception)
            {
                return TempDataView();
            }
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
                return this.RedirectToAction(nameof(AllCountries), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllPropertyTypes()
        {
            try
            {
                var model = await this.adminService.GetAllPropertyTypesAsync();
                return this.View(model);
            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public IActionResult AddPropertyType()
        {
            return this.View(new AddNonExistingPropertyTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddPropertyType(AddNonExistingPropertyTypeViewModel propertyType)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(propertyType);
            }

            try
            {
                bool propertyTypeExist = await this.adminService.AddNewPropertyTypeAsync(propertyType);

                if (propertyTypeExist)
                {
                    this.TempData["InvalidPropertyTypeAdded"] = "This property type already exists";
                    return this.View();
                }

                this.TempData["CountrySuccessfullyAdded"] = "The property type was successfully added";
                //Add this tempdata to the AllPropertyTypes
                return this.RedirectToAction(nameof(AllPropertyTypes), "Dashboard", new { area = Administrator });
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
