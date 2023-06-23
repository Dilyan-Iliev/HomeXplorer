namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class CloudImage
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; } = null!;

        public Guid PropertyId { get; set; }

        public virtual Property Property { get; set; } = null!;
    }
}
