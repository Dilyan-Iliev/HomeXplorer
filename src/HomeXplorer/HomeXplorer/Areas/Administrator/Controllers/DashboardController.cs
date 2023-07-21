namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Admin;
    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;
    using static HomeXplorer.Common.TempDataConstants;

    public class DashboardController : BaseAdminController
    {
        private readonly IAdminService adminService;
        private readonly ICountryService countryService;
        private readonly IAgentPropertyService propertyService;

        public DashboardController(
            IAdminService adminService,
            ICountryService countryService,
            IAgentPropertyService propertyService)
        {
            this.adminService = adminService;
            this.countryService = countryService;
            this.propertyService = propertyService;
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
        public async Task<IActionResult> AllCities()
        {
            try
            {
                var model = await this.adminService.GetAllCitiesFromCountryAsync();
                return this.View(model);

            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddCity()
        {
            var model = new AddNonExistingCityToExistingCountryViewModel()
            {
                Countries = await this.countryService.GetCountriesAsync()
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(AddNonExistingCityToExistingCountryViewModel city)
        {
            if (!this.ModelState.IsValid)
            {
                city.Countries = await this.countryService.GetCountriesAsync();
                return this.View(city);
            }

            bool selectedCountryIdExist = await this.propertyService.ExistByIdAsync<Country>(city.CountryId);

            if (!selectedCountryIdExist)
            {
                this.TempData["InvalidDropdownOption"] = "You must choose a valid option from the dropdowns";
                city.Countries = await this.countryService.GetCountriesAsync();
                return this.View(city);
            }

            try
            {
                bool cityExist = await this.adminService.AddNewCityAsync(city);
                if (cityExist)
                {
                    this.TempData["InvalidCityAdded"] = "This city already exists";
                    return this.View();
                }

                this.TempData["CitySuccessfullyAdded"] = "The city was successfully added";
                return this.RedirectToAction(nameof(AllCities), "Dashboard", new { area = Administrator });

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

                this.TempData["PropertyTypeSuccessfullyAdded"] = "The property type was successfully added";
                return this.RedirectToAction(nameof(AllPropertyTypes), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllBuildingTypes()
        {
            try
            {
                var model = await this.adminService.GetAllBuildingTypesAsync();
                return this.View(model);

            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public IActionResult AddBuildingType()
        {
            return this.View(new AddNonExistingBuildingTypeViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddBuildingType(AddNonExistingBuildingTypeViewModel buildingType)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(buildingType);
            }

            try
            {
                bool buildingTypeExist = await this.adminService.AddNewBuildingTypeAsync(buildingType);

                if (buildingTypeExist)
                {
                    this.TempData["InvalidBuildingTypeAdded"] = "This building type already exists";
                    return this.View();
                }

                this.TempData["BuildingTypeSuccessfullyAdded"] = "The building type was successfully added";
                return this.RedirectToAction(nameof(AllBuildingTypes), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> PendingReviews()
        {
            try
            {
                var reviews = await this.adminService.GetAllPendingReviewsAsync();

                return this.View(reviews);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApproveReview(int id)
        {
            try
            {
                await this.adminService.ApproveReviewAsync(id);
                this.TempData[SuccessfullyApproved] = SuccessfullyApproved;
                return this.RedirectToAction(nameof(PendingReviews), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await this.adminService.DeleteReviewAsync(id);
                this.TempData["SuccessfullyDeleted"] = "The review was successfully removed";
                return this.RedirectToAction(nameof(PendingReviews), "Dashboard", new { area = Administrator });
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllAgents()
        {
            try
            {
                var agentsStatistics = await this.adminService.GetAllAgentsStatisticsAsync();
                return this.View(agentsStatistics);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllRenters()
        {
            try
            {
                var rentersStatistics = await this.adminService.GetAllRentersStatisticsAsync();
                return this.View(rentersStatistics);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        private IActionResult TempDataView()
        {
            this.TempData["DashboardError"] = "Something went wrong, please try again";
            return this.RedirectToAction(nameof(Index), "Dashboard", new { area = Administrator });
        }
    }
}