namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

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
            try
            {
                var model = await this.renterPropertyService.AllAsync(pageNumber, pageSize, propertySorting);

                return this.View(model);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllNearby(int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            string userId = this.User.GetId();

            try
            {
                var model = await this.renterPropertyService
                    .AllNearbyAsync(pageNumber, pageSize, propertySorting, userId);

                return this.View(model);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            string userId = this.User.GetId();

            try
            {
                var property = await this.renterPropertyService.GetPropertyDetailsAsync(id, userId);

                if (property == null)
                {
                    this.TempData["DetailsError"] = "Can't show the details of the property";
                    return this.RedirectToAction("Index", "Home", new { area = UserRoleConstants.Renter });
                }

                return this.View(property);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(Guid id)
        {
            string userId = this.User.GetId();

            try
            {
                await this.renterPropertyService.AddToFavoritesAsync(id, userId);
                this.TempData["SuccessfullyAddedToFavs"] = "The property was successfully added to favorites";
                return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
            }
            catch (Exception)
            {
                this.TempData["UnexpectedError"] = "Something went wrong, please try again";
                return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(Guid id)
        {
            string userId = this.User.GetId();

            try
            {
                await this.renterPropertyService.RemoveFromFavoritesAsync(id, userId);
                this.TempData["SuccessfullyRemovedFromFavs"] = "The property was successfully removed from favorites";
                return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
            }
            catch (Exception)
            {
                this.TempData["UnexpectedError"] = "Something went wrong, please try again";
                return this.RedirectToAction(nameof(Favorites), "Property", new { area = UserRoleConstants.Renter });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Rent(Guid id)
        {
            string userId = this.User.GetId();

            try
            {
                await this.renterPropertyService.RentAsync(id, userId);
                this.TempData["SuccessfullyRented"] = "The property was successfully rented";
                return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
            }
            catch (Exception)
            {
                this.TempData["UnexpectedError"] = "Something went wrong, please try again";
                return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Leave(Guid id)
        {
            string userId = this.User.GetId();

            try
            {
                await this.renterPropertyService.LeaveAsync(id, userId);
                this.TempData["SuccessfullyLeft"] = "The property was successfully left";
                return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
            }
            catch (Exception)
            {
                this.TempData["UnexpectedError"] = "Something went wrong, please try again";
                return this.RedirectToAction(nameof(Rented), "Property", new { area = UserRoleConstants.Renter });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            string userId = this.User.GetId();

            try
            {
                var favProperties =
                    await this.renterPropertyService.GetAllFavoritesAsync(userId);

                if (favProperties == null)
                {
                    return this.TempDataView();
                }

                return this.View(favProperties);

            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Rented()
        {
            string userId = this.User.GetId();

            try
            {
                var rentedProperties = 
                    await this.renterPropertyService.GetAllRentedAsync(userId);

                if (rentedProperties == null)
                {
                    return this.TempDataView();
                }

                return this.View(rentedProperties);
            }
            catch (Exception)
            {
                return this.TempDataView();
            }
        }

        private IActionResult TempDataView()
        {
            this.TempData["UnexpectedError"] = "Something went wrong, please try again";
            return this.View();
        }
    }
}
