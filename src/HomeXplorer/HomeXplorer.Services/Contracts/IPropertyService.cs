namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property;

    public interface IPropertyService
    {
        Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId);
    }
}
