﻿namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("Agent who offers the property")]
    public class Agent
    {
        public Agent()
        {
            this.Properties = new HashSet<Property>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; } //TODO: switch with Guid

        [Comment("Reference to the IdentityUser")]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        [Comment("Reference to the City")]
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        [Comment("City of the agent")]
        public City City { get; set; } = null!;

        [Comment("The associated IdentityUser")]
        public virtual ApplicationUser User { get; set; } = null!;

        [Comment("The profile picture of the agent")]
        [Required]
        public string ProfilePictureUrl { get; set; } = null!;

        [Comment("Properties offered by the agent")]
        public virtual ICollection<Property> Properties { get; set; }
    }
}
