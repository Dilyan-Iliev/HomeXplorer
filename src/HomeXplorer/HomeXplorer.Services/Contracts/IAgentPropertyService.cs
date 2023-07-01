namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Agent;

    public interface IAgentPropertyService
    {
        Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId);

        Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId);

        Task<DetailsPropertyViewModel?> GetDetailsAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
