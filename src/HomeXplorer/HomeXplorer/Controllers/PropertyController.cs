namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Microsoft.AspNetCore.Authorization;

    using static HomeXplorer.Common.UserRoleConstants;

    public class PropertyController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(Guid id)
        {
            //var property = await this.renterPropertyService.GetBasePropertyDetailsAsync(id);

            //if (property == null)
            //{
            //    this.TempData["DetailsError"] = "Can't show the details of the property";
            //    return this.RedirectToAction("Index", "Home");
            //}
            return this.RedirectToAction("Details", "Property", new { area = Renter });
            //return this.View(property);
        }
    }
}
