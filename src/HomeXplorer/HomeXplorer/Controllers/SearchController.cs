namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    public class SearchController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Result()
        {
            return View();
        }
    }
}
