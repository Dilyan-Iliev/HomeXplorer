namespace HomeXplorer.ViewModels.Property.Renter
{
    public class LatestPropertiesViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Size { get; set; }

        public string City { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string AddedOn { get; set; } = null!;

        public int Visits { get; set; }

        public string CoverImageUrl { get; set; } = null!;
    }
}