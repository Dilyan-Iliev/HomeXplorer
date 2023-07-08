namespace HomeXplorer.ViewModels.Property.Renter
{
    using HomeXplorer.ViewModels.Property.Enums;

    public class RenterAllPropertiesViewModel
    {
        public RenterAllPropertiesViewModel()
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
