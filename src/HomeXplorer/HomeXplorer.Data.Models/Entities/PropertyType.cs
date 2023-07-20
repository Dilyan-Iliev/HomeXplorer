namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

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
