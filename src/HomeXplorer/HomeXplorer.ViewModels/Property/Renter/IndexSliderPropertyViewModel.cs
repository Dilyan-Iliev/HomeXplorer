namespace HomeXplorer.ViewModels.Property.Renter
{
    public class IndexSliderPropertyViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal Price { get; set; }

        public string CoverImageUrl { get; set; } = null!;
    }
}
