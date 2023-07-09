namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using HomeXplorer.ViewModels.Search;

    public class SearchController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Result()
        {
            return View();
        }
    }
}
