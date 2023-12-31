﻿namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.ViewModels.Property.Agent;
    using HomeXplorer.ViewModels.Property.Enums;

    public interface IAgentPropertyService
    {
        Task AddAsync(AddPropertyViewModel model, ICollection<string> imageUrls, string userId);

        Task<IEnumerable<IndexAgentPropertiesViewModel>> GetLastThreeAsync(string userId);

        Task<DetailsPropertyViewModel?> GetDetailsAsync(Guid id);

        Task<bool> DeleteAsync(Guid id, string userId);

        Task EditAsync(EditPropertyViewModel model, Guid propertyId,
            ICollection<string>? imageUrls, ICollection<CloudImage> oldImages, ICollection<int> deletedPhotosIds);

        Task<EditPropertyViewModel?> FindByIdAsync(Guid propertyId);

        Task<IEnumerable<PropertyImagesViewModel>> GetAllImageUrlsForPropertyAsync(Guid propertyId);

        Task<AgentAllPropertiesViewModel> AllAsync(int pageNumber, int pageSize, PropertySorting propertySorting, string userId);

        Task<bool> ExistByIdAsync<T>(object id) where T : class;
    }
}
