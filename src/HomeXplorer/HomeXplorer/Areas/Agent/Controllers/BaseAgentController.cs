namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static HomeXplorer.Common.UserRoleConstants;

    [Area(Agent)]
    [Authorize(Roles = Agent)]
    public class BaseAgentController : Controller
    {
    }
}
