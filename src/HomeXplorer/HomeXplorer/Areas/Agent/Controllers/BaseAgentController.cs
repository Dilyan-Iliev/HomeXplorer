namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static HomeXplorer.Common.UserRoleConstants;

    [Authorize]
    [Area(Agent)]
    public class BaseAgentController : Controller
    {
    }
}
