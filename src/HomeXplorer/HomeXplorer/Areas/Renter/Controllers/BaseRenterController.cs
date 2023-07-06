namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using static HomeXplorer.Common.UserRoleConstants;

    [Area(Renter)]
    [Authorize(Roles = Renter)]
    public class BaseRenterController : Controller
    {
    }
}
