﻿namespace HomeXplorer.Data.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("Country of the property")]
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the country")]
        [Required]
        public string Name { get; set; } = null!;

        [Comment("The cities in the country")]
        public virtual ICollection<City> Cities { get; set; }
    }
}
