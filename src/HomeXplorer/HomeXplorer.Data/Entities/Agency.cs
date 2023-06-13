namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Agency of the agent")]
    public class Agency
    {
        public Agency()
        {
            this.Agents = new HashSet<Agent>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the agency")]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("Email of the agency")]
        [Required]
        public string Email { get; set; } = null!;

        [Comment("Address of the agency")]
        [Required]
        public string Address { get; set; } = null!;

        public virtual ICollection<Agent> Agents { get; set; }
    }
}
