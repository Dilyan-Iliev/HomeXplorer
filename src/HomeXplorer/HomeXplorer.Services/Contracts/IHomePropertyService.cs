namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Renter;

    public interface IHomePropertyService
    {
        Task<IEnumerable<LatestPropertiesViewModel>> GetAllPropertiesAsync();
    }
}
