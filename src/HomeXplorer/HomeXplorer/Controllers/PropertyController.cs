namespace HomeXplorer.Controllers
{
    using HomeXplorer.Common;
    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class PropertyController : BaseController
    {
        private readonly IRenterPropertyService renterPropertyService;

        public PropertyController(IRenterPropertyService renterPropertyService)
        {
            this.renterPropertyService = renterPropertyService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var property = await this.renterPropertyService.GetBasePropertyDetailsAsync(id);

            if (property == null)
            {
                this.TempData["DetailsError"] = "Can't show the details of the property";
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(property);
        }
    }
}
