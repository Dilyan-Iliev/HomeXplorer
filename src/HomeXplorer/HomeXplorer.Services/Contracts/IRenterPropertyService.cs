namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Renter;

    public interface IRenterPropertyService
    {
        Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync();

        Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreePropertiesNearbyAsync(string userId);

        Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreeAddedPropertiesAsync();

        Task<RenterDetailsPropertyViewModel> GetPropertyDetailsAsync(Guid id, string? userId = null);

        Task AddToFavoritesAsync(Guid propertyId, string userId);

        Task RemoveFromFavoritesAsync(Guid propertyId, string userId);

        Task RentAsync(Guid propertyId, string userId);

        Task LeaveAsync(Guid propertyId, string userId);

        Task<RenterAllPropertiesViewModel> AllAsync(int pageNumber, int pageSize, PropertySorting propertySorting);

        Task<RenterAllPropertiesViewModel> AllNearbyAsync(int pageNumber, int pageSize,
            PropertySorting propertySorting, string userId);

        Task<IEnumerable<LatestPropertiesViewModel>> GetAllFavoritesAsync(string userId);

        Task<IEnumerable<LatestPropertiesViewModel>> GetAllRentedAsync(string userId);
    }
}
