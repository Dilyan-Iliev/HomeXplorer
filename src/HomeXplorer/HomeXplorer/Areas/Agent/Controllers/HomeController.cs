namespace HomeXplorer.Areas.Agent.Controllers
{
    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseAgentController
    {
        private readonly IPropertyService propertyService;

        public HomeController(IPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.User.GetUserId();

            var properties = await this.propertyService.GetLastThreeAsync(userId);
            return View(properties);
        }
    }
}
