namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.BuildingType;

    public interface IBuildingTypeService
    {
        Task<IEnumerable<SelectBuildingTypeViewModel>> GetBuildingTypesAsync();
    }
}
