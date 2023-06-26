namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Offered property")]
    public class Property
    {
        public Property()
        {
            this.Id = Guid.NewGuid();
            this.Images = new HashSet<CloudImage>();
        }

        [Key]
        [Comment("Primary key")]
        public Guid Id { get; set; }

        [Comment("Name of the property")]
        public string? Name { get; set; }

        [Comment("Desription of the property")]
        [Required]
        public string Description = null!;

        [Comment("Price of the property")]
        public decimal Price { get; set; }

        [Comment("Size of the property (square meters)")]
        public int Size { get; set; }

        [Comment("Allowed/not allowed pets in the property")]
        public bool PetsAllowed { get; set; }

        [Comment("Address of the property")]
        [Required]
        public string Address { get; set; } = null!;

        [Comment("Is the property active or not")]
        public bool IsActive { get; set; } = true;

        [Comment("Time when property offer is being added")]
        [Required]
        public DateTime AddedOn { get; set; }

        [Comment("Time when property offer is being edited")]
        [Required]
        public DateTime ModifiedOn { get; set; }

        [Comment("Country ID of the property")]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        [Comment("Country of the property")]
        public virtual Country Country { get; set; }

        [Comment("City ID of the property")]
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        [Comment("City of the property")]
        public virtual City City { get; set; } = null!;

        [Comment("Type ID of the property")]
        [ForeignKey(nameof(PropertyType))]
        public int PropertyTypeId { get; set; }

        [Comment("Type of the property")]
        public virtual PropertyType PropertyType { get; set; } = null!;

        [Comment("Status ID of the property")]
        [ForeignKey(nameof(PropertyStatus))]
        public int PropertyStatusId { get; set; }

        [Comment("Status of the property")]
        public virtual PropertyStatus PropertyStatus { get; set; } = null!;

        [Comment("Building type ID of the property")]
        [ForeignKey(nameof(BuildingType))]
        public int BuildingTypeId { get; set; }

        [Comment("Building type of the property")]
        public virtual BuildingType BuildingType { get; set; } = null!;

        [Comment("Agent ID of the property")]
        [ForeignKey(nameof(Agent))]
        public int AgentId { get; set; }

        [Comment("Agent of the property")]
        public virtual Agent Agent { get; set; } = null!;

        [Comment("Property renter ID")]
        [ForeignKey(nameof(Renter))]
        public int? RenterId { get; set; }

        [Comment("Property renter")]
        public virtual Renter? Renter { get; set; }

        public virtual ICollection<CloudImage> Images { get; set; }
    }
}
