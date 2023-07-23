namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Renter;
    using HomeXplorer.Core.Contexts;

    public class ReviewService
        : IReviewService
    {
        private readonly HomeXplorerDbContext dbContext;

        public ReviewService(HomeXplorerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(AddReviewViewModel model, string userId)
        {
            Renter? renter = await this.RetrieveRenterAsync(userId);

            Review review = new Review()
            {
                ReviewCreatorId = renter!.Id,
                AddedOn = DateTime.UtcNow,
                Description = model.Description
            };

            await this.dbContext.Reviews.AddAsync(review);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IndexReviewViewModel>> GetAllReviewsAsync()
        {
            return await this.dbContext
                .Reviews
                .AsNoTracking()
                .Where(r => r.IsApproved)
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
            return await this.dbContext
                            .Renters
                            .AsNoTracking()
                            .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
