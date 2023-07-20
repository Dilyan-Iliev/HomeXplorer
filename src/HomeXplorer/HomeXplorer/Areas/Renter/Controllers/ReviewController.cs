namespace HomeXplorer.Areas.Renter.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Extensions;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;

    using static HomeXplorer.Common.UserRoleConstants;

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

                return this.RedirectToAction("Index", "Home", new { area = Renter });
            }
            catch (Exception)
            {
                this.TempData["ReviewError"] = "Something went wrong, please try again";
                return this.View(model);
            }
        }
    }
}
