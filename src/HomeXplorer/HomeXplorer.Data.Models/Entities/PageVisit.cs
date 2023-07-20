namespace HomeXplorer.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

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
    }
}
