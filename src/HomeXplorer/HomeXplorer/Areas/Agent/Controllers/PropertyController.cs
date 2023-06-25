namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PropertyController : BaseAgentController
    {
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }
    }
}
