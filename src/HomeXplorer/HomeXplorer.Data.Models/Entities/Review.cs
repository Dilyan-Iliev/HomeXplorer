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

        [Comment("Date and time when the review was added")]
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

        [Comment("Reviewer ID")]
        [Required]
        [ForeignKey(nameof(ReviewCreator))]
        public int ReviewCreatorrId { get; set; }

        [Comment("Creator of the review")]
        public Renter ReviewCreator { get; set; } = null!;

        //[Comment("Indicates if the review is approved")]
        //public bool IsApproved { get; set; } = false;

        //[Comment("Approver ID")]
        //[ForeignKey(nameof(Approver))]
        //public string? ApproverId { get; set; }

        //[Comment("User who approved the review")]
        //public ApplicationUser? Approver { get; set; }
    }
}
