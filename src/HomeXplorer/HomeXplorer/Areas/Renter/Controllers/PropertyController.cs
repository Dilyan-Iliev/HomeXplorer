namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Services.Contracts;

    using static HomeXplorer.Common.UserRoleConstants;

    public class PropertyController : BaseRenterController
    {
        private readonly IRenterPropertyService renterPropertyService;

        public PropertyController(IRenterPropertyService renterPropertyService)
        {
            this.renterPropertyService = renterPropertyService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var property = await this.renterPropertyService.GetPropertyDetailsAsync(id);

            if (property == null)
            {
                this.TempData["DetailsError"] = "Can't show the details of the property";
                return this.RedirectToAction("Index", "Home", new { area = Renter });
            }

            return this.View(property);
        }
    }
}
