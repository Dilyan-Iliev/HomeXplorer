namespace HomeXplorer.ViewModels.Search
{
    using HomeXplorer.ViewModels.Property.Enums;
    using HomeXplorer.ViewModels.Property.Renter;

    public class SearchResultViewModel
    {
        public SearchResultViewModel()
        {
            this.Properties = new List<LatestPropertiesViewModel>();
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; } //properties per page

        public int TotalPages { get; set; }

        public PropertySorting PropertySorting { get; set; }

        public IEnumerable<LatestPropertiesViewModel> Properties { get; set; }
    }
}
