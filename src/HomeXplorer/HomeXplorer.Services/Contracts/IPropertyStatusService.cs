namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.PropertyStatus;

    public interface IPropertyStatusService
    {
        Task<IEnumerable<SelectPropertyStatusViewModel>> GetPropertyStatusesAsync();
    }
}
