namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("City where the property is")]
    public class City
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the city")]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("Country ID of the property")]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        [Comment("Country of the property")]
        public virtual Country Country { get; set; } = null!;
    }
}
