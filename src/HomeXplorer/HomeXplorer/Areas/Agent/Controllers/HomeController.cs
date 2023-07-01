namespace HomeXplorer.Areas.Agent.Controllers
{
    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseAgentController
    {
        private readonly IAgentPropertyService propertyService;

        public HomeController(IAgentPropertyService propertyService)
        {
            this.propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.User.GetId();

            var properties = await this.propertyService.GetLastThreeAsync(userId);
            return View(properties);
        }
    }
}
