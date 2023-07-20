namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Status of the property")]
    public class PropertyStatus
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the property status")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
