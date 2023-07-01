namespace HomeXplorer.ViewModels.Property.Agent
{
    public class DetailsPropertyViewModel
    {
        public DetailsPropertyViewModel()
        {
            this.Images = new List<PropertyImagesViewModel>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int Size { get; set; }

        public string PropertyType { get; set; } = null!;

        public string BuildingType { get; set; } = null!;

        public string PropertyStatus { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string AddedOd { get; set; } = null!;

        public IEnumerable<PropertyImagesViewModel> Images { get; set; }

        //something for google maps

        //TODO:
        //renter and not loged-in user must see agent details
    }
}
