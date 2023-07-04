namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.ViewModels.Property.Agent.Enums;

    public interface IAgentPropertyService
    {
        Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId);

        Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId);

        Task<DetailsPropertyViewModel?> GetDetailsAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task EditAsync(EditPropertyViewModel model, Guid propertyId,
            ICollection<string>? imageUrls, ICollection<CloudImage> oldImages, ICollection<int> deletedPhotosIds);

        Task<EditPropertyViewModel?> FindByIdAsync(Guid propertyId);

        Task<IEnumerable<PropertyImagesViewModel>> GetAllImageUrlsForPropertyAsync(Guid propertyId);

        Task<AllPropertiesViewModel> AllAsync(PropertySorting propertySorting);
    }
}
