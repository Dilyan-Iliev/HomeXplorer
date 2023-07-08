namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.ViewModels.Property.Renter;
    using HomeXplorer.Services.Contracts;

    public class ReviewController : BaseRenterController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult AddReview()
        {
            return View(new AddReviewViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                string userId = this.User.GetId();
                await this.reviewService.AddAsync(model, userId);

                this.TempData["ReviewApprove"] = "Your review awaits approve from administrator";

                return this.Ok(); //change redirection
            }
            catch (Exception)
            {
                //Add tempdata message and redirection
                return this.BadRequest();
            }
        }
    }
}
