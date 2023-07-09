namespace HomeXplorer.Services.Contracts
{
    using HomeXplorer.ViewModels.Search;

    public interface ISearchService
    {
        Task<PropertySearchViewModel> FillPropertySearchbarAsync();
    }
}
