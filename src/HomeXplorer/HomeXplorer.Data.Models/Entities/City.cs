namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("City where the property is")]
    public class City
    {
        public City()
        {
            this.Properties = new HashSet<Property>();
            this.Renters = new HashSet<Renter>();
            this.Agents = new HashSet<Agent>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the city")]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("Country ID of the city")]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        [Comment("Country of the city")]
        public virtual Country Country { get; set; } = null!;

        [Comment("Properties in the city")]
        public virtual ICollection<Property> Properties { get; set; }

        [Comment("Agents in the city")]
        public virtual ICollection<Renter> Renters { get; set; }

        [Comment("Renters in the city")]
        public virtual ICollection<Agent> Agents { get; set; }
    }
}
