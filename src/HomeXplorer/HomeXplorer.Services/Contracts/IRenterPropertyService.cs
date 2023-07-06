namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Renter;

    public interface IRenterPropertyService
    {
        Task<IEnumerable<IndexSliderPropertyViewModel>> GetLastThreeAddedForSliderAsync();
    }
}
