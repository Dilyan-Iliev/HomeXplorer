namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Type of the property")]
    public class PropertyType
    {
        [Key]
        public int Id { get; set; }

        [Comment("Name of the property status")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
