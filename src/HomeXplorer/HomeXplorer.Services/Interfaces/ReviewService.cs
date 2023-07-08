namespace HomeXplorer.Services.Interfaces
{
    using System.Threading.Tasks;

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
                ReviewCreatorrId = renter!.Id,
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

        private async Task<Renter?> RetrieveRenterAsync(string userId)
        {
            return await this.repo
                            .AllReadonly<Renter>()
                            .FirstOrDefaultAsync(r => r.UserId == userId);
        }
    }
}
