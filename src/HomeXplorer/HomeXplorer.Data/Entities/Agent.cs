namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Agent who offers the property")]
    public class Agent
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("First name of the agent")]
        [Required]
        public string FirstName { get; set; } = null!;

        [Comment("Last name of the agent")]
        [Required]
        public string LastName { get; set; } = null!;

        [Comment("Phone number of the agent")]
        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Comment("Email of the agent")]
        [Required]
        public string Email { get; set; } = null!;

        [Comment("Rating of the agent")]
        public decimal Rating { get; set; }

        [Comment("Agency ID of the agent")]
        [ForeignKey(nameof(Agency))]
        public int AgencyId { get; set; }

        [Comment("Agency of the agent")]
        public virtual Agency Agency { get; set; } = null!;
    }
}
