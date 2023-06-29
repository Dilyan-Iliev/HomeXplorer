namespace HomeXplorer.ViewModels.Property.Agent
{
    public class IndexAgentPropertiesViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Size { get; set; }

        public string Status { get; set; } = null!;

        public int Visits { get; set; }

        public string AddedOn { get; set; } = null!;

        public string CoverPhotoUrl { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
