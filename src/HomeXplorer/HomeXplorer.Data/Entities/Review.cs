namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Review of a renter")]
    public class Review
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Review description")]
        [Required]
        public string Description { get; set; } = null!;

        [Comment("Reviewer ID")]
        [Required]
        [ForeignKey(nameof(Reviewer))]
        public string ReviewerId { get; set; } = null!;

        [Comment("Creator of the review")] //this is the Renter
        public Renter Reviewer { get; set; } = null!;
    }
}
