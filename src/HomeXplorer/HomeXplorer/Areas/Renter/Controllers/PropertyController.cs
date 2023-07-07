namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;
    using HomeXplorer.Extensions;

    public class PropertyController : BaseRenterController
    {
        private readonly IRenterPropertyService renterPropertyService;

        public PropertyController(IRenterPropertyService renterPropertyService)
        {
            this.renterPropertyService = renterPropertyService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            string userId = this.User.GetId();
            var property = await this.renterPropertyService.GetPropertyDetailsAsync(id, userId);

            if (property == null)
            {
                this.TempData["DetailsError"] = "Can't show the details of the property";
                return this.RedirectToAction("Index", "Home", new { area = Renter });
            }

            return this.View(property);
        }

        public async Task<IActionResult> AddFavorite(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.AddToFavoritesAsync(id, userId);

            return Ok(); //switch to redirect to action to MyFavorites
        }

        public async Task<IActionResult> Rent(Guid id)
        {
            string userId = this.User.GetId();

            await this.renterPropertyService.RentAsync(id, userId);

            return this.Ok();
        }
    }
}
