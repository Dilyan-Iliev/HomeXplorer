namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseAgentController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
