namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Page visits")]
    public class PageVisit
    {
        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("URL that is being visited")]
        public string Url { get; set; } = null!;

        [Comment("Count of visits")]
        public int VisitsCount { get; set; }

        [Comment("Hashed value of the cookie")]
        public string HashedVisitCookie { get; set; } = null!;
    }
}
