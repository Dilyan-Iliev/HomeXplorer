namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static HomeXplorer.Common.UserRoleConstants;

    [Area(Administrator)]
    [Authorize(Roles = Administrator)]
    public class BaseAdminController : Controller
    {
    }
}
