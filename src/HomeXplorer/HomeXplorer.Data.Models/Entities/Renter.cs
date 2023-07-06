namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HomeXplorer.Data.Models.Entities;

    [Comment("Renter of the property")]
    public class Renter
    {
        public Renter()
        {
            this.RentedProperties = new HashSet<Property>();
            this.FavouriteProperties = new HashSet<RenterPropertyFavorite>();
            this.Reviews = new HashSet<Review>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Refference to the Identity User")]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Comment("The associated IdentityUser")]
        public ApplicationUser User { get; set; } = null!;

        [Comment("The associated City")]
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        [Comment("City of the renter")]
        public City City { get; set; } = null!;

        [Comment("Profile picture of the renter")]
        [Required]
        public string ProfilePictureUrl { get; set; } = null!;

        [Comment("Rented properties")]
        //[InverseProperty(nameof(Property.Renter))]
        public virtual ICollection<Property>? RentedProperties { get; set; }

        [Comment("Favourite properties")]
        public virtual ICollection<RenterPropertyFavorite>? FavouriteProperties { get; set; }

        [Comment("Renter reviews")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}