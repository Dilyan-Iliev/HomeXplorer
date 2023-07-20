namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    [Comment("Building type of the property")]
    public class BuildingType
    {
        [Key]
        public int Id { get; set; }

        [Comment("Name of the building type")]
        [Required]
        public string Name { get; set; } = null!;
    }
}
