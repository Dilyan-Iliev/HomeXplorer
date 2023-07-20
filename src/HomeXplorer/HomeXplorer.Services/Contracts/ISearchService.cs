namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Search;
    using HomeXplorer.ViewModels.Property.Enums;

    public interface ISearchService
    {
        Task<PropertySearchViewModel> FillPropertySearchbarAsync();

        Task<SearchResultViewModel> SearchResult(PropertySearchViewModel model, int pageNumber, int pageSize, PropertySorting propertySorting);
    }
}
