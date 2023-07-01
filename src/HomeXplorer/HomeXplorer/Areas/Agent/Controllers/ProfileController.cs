namespace HomeXplorer.Areas.Agent.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController 
        : BaseAgentController
    {
        [HttpGet]
        public IActionResult MyProfile()
        {
            return this.View();
        }
    }
}
