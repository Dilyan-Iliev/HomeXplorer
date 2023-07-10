namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Search;

    public interface ISearchService
    {
        Task<PropertySearchViewModel> FillPropertySearchbarAsync();

        Task<SearchResultViewModel> SearchResult(PropertySearchViewModel model, int pageNumber, int pageSize, PropertySorting propertySorting);
    }
}
