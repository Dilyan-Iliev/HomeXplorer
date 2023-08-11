namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;

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
            try
            {
                string userId = this.User.GetId();

                var properties = await this.propertyService.GetLastThreeAsync(userId);
                return View(properties);
            }
            catch (Exception)
            {
                this.TempData["IndexError"] = "Something went wrong - please try again";
                return this.View();
            }
        }
    }
}
