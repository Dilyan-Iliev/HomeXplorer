namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.PropertyType;

    public interface IPropertyTypeService
    {
        Task<IEnumerable<SelectPropertyTypeViewModel>> GetPropertyTypesAsync();
    }
}
