namespace HomeXplorer.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
