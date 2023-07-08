namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Renter;

    public interface IReviewService
    {
        Task AddAsync(AddReviewViewModel model, string userId);

        Task<IEnumerable<IndexReviewViewModel>> GetAllReviewsAsync();
    }
}
