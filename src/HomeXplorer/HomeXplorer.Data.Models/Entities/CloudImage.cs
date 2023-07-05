namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("Image of the property")]
    public class CloudImage
    {
        [Key]
        [Comment("Primary key")]
        public int Id { get; set; }

        [Comment("Url to the cloudinary")]
        public string Url { get; set; } = null!;
      
        [Comment("Property Id of the Image")]
        [ForeignKey(nameof(Property))]
        public Guid? PropertyId { get; set; } //todo: make not nullable

        [Comment("Property on the images")]
        public virtual Property? Property { get; set; }

        
    }
}
