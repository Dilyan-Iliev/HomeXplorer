namespace HomeXplorer.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using HomeXplorer.Services.Contracts;

    public class PropertySearchViewComponent
        : ViewComponent
    {
        private readonly ISearchService searchService;

        public PropertySearchViewComponent(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await this.searchService.FillPropertySearchbarAsync();

            return this.View(model);
        }
    }
}
