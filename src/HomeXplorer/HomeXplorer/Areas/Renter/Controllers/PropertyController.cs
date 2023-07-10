namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Services.Contracts;

    using HomeXplorer.Extensions;
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Common;

    public class PropertyController : BaseRenterController
    {
        private readonly IRenterPropertyService renterPropertyService;

        public PropertyController(IRenterPropertyService renterPropertyService)
        {
            this.renterPropertyService = renterPropertyService;
        }

        [HttpGet]
        public async Task<IActionResult> AllProperties(int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            var model = await this.renterPropertyService.AllAsync(pageNumber, pageSize, propertySorting);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllNearby(int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            string userId = this.User.GetId();

            var model = await this.renterPropertyService.AllNearbyAsync(pageNumber, pageSize, propertySorting, userId);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string userId = this.User.GetId();
            var property = await this.renterPropertyService.GetPropertyDetailsAsync(id, userId);

            if (property == null)
            {
                this.TempData["DetailsError"] = "Can't show the details of the property";
                return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Renter });
            }

            return this.View(property);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.AddToFavoritesAsync(id, userId);
            //add tempdata message
            return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
        }

        [HttpPost]
        public async Task<IActionResult> Rent(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.RentAsync(id, userId);
            //add tempdata message
            return this.Ok(); //switch to redirect to action to MyRented
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            string userId = this.User.GetId();

            var favProperties = await this.renterPropertyService.GetAllFavoritesAsync(userId);

            if (favProperties == null)
            {

            }

            return this.View(favProperties);
        }

        //[HttpGet]
        //public async Task<IActionResult> Rented()
        //{
        //    string userId = this.User.GetId();


        //}
    }
}
