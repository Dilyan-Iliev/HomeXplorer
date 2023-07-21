namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using static HomeXplorer.Common.UserRoleConstants;

    [Area(Administrator)]
    [Authorize(Roles = Administrator)]
    public class BaseAdminController : Controller
    {
    }
}
