namespace HomeXplorer.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Reviews = new HashSet<Review>();
        }

        [Comment("First name of the user")]
        [Required]
        public string FirstName { get; set; } = null!;

        [Comment("Last name of the user")]
        [Required]
        public string LastName { get; set; } = null!;

        public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
