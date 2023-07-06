namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Renter;

    public interface IRenterPropertyService
    {
        Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync();

        Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreePropertiesNearbyAsync(string userId);

        Task<IEnumerable<LatestPropertiesViewModel>> GetLastThreeAddedPropertiesAsync();
    }
}
