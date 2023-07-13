namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Common;
    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Enums;

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
        public async Task<IActionResult> RemoveFavorite(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.RemoveFromFavoritesAsync(id, userId);
            //add tempdata message
            return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
        }

        [HttpPost]
        public async Task<IActionResult> Rent(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.RentAsync(id, userId);
            //add tempdata message
            return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
        }

        [HttpPost]
        public async Task<IActionResult> Leave(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.LeaveAsync(id, userId);
            //add tempdata message
            return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
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

        [HttpGet]
        public async Task<IActionResult> Rented()
        {
            string userId = this.User.GetId();

            var rentedProperties = await this.renterPropertyService.GetAllRentedAsync(userId);

            if (rentedProperties == null)
            {

            }

            return this.View(rentedProperties);
        }
    }
}
