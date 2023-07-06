namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseRenterController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
