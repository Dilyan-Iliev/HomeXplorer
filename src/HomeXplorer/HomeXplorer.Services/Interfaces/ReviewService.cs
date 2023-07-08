namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;

    public class ReviewService
        : IReviewService
    {
        private readonly IRepository repo;

        public ReviewService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddAsync(AddReviewViewModel model, string userId)
        {
            Renter? renter = await this.RetrieveRenterAsync(userId);

            Review review = new Review()
            {
                ReviewCreatorId = renter!.Id,
                AddedOn = DateTime.UtcNow,
                Description = model.Description,
                ReviewCreator = renter,
            };

            //TODO: added review must be approved from admin in order to render on home page
            try
            {
                await this.repo.AddAsync<Review>(review);
                await this.repo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IndexReviewViewModel>> GetAllReviewsAsync()
        {
            return await this.repo
                .AllReadonly<Review>()
                //add where for only approved reviews
                .Select(r => new IndexReviewViewModel()
                {
                    Description = r.Description,
                    ReviewCreatorName = $"{r.ReviewCreator.User.FirstName} {r.ReviewCreator.User.LastName}",
                    ReviewCreatorAvatarUrl = r.ReviewCreator.ProfilePictureUrl
                })
                .ToListAsync();
        }

        private async Task<Renter?> RetrieveRenterAsync(string userId)
        {
            return await this.repo
                            .AllReadonly<Renter>()
                            .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
