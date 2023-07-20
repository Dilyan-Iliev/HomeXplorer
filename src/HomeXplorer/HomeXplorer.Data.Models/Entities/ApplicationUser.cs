namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    [Comment("Extended Identity User")]
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //    this.ApprovedReviews = new HashSet<Review>();
        //}

        [Comment("First name of the user")]
        [Required]
        public string FirstName { get; set; } = null!;

        [Comment("Last name of the user")]
        [Required]
        public string LastName { get; set; } = null!;

        [Comment("Date and time when the user is being registered")]
        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        //public virtual ICollection<Review> ApprovedReviews { get; set; }
    }
}
