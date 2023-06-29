namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Agent;

    public interface IPropertyService
    {
        Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId);

        Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId);
    }
}
