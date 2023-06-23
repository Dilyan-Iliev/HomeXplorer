namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Agent who offers the property")]
    public class Agent
    {
        public Agent()
        {
            this.Properties = new HashSet<Property>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        //[Comment("First name of the agent")]
        //[Required]
        //public string FirstName { get; set; } = null!;

        //[Comment("Last name of the agent")]
        //[Required]
        //public string LastName { get; set; } = null!;

        //[Comment("Phone number of the agent")]
        //public string? PhoneNumber { get; set; }

        //[Comment("Rating of the agent")]
        //public decimal Rating { get; set; }

        //[Comment("Email address of the agent")]
        //public string Email { get; set; }

        [Comment("Reference to the IdentityUser")]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [Comment("The associated IdentityUser")]
        public virtual ApplicationUser User { get; set; }

        //[Comment("Agency ID of the agent")]
        //[ForeignKey(nameof(Agency))]
        //public int AgencyId { get; set; }

        //[Comment("Agency of the agent")]
        //public virtual Agency Agency { get; set; } = null!;

        [Comment("Properties offered by the agent")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
