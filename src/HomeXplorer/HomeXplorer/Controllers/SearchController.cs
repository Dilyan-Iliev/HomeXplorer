namespace HomeXplorer.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using HomeXplorer.ViewModels.Search;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.ViewModels.Property.Enums;

    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Result([FromQuery] PropertySearchViewModel searchResultModel,
            int pageNumber = 1, int pageSize = 3,
            PropertySorting propertySorting = PropertySorting.Default)
        {
            var resultModel =
                await this.searchService.SearchResult(searchResultModel, pageNumber, pageSize, propertySorting);
            return View(resultModel);
        }
    }
}
