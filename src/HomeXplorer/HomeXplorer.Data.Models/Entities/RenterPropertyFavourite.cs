namespace HomeXplorer.Data.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Data.Entities;

    [Comment("Linking table representing favorite properties for renters")]
    public class RenterPropertyFavorite
    {
        [ForeignKey(nameof(Renter))]
        public int? RenterId { get; set; }

        public virtual Renter? Renter { get; set; }

        [ForeignKey(nameof(Property))]
        public Guid? PropertyId { get; set; }

        public virtual Property? Property { get; set; }
    }
}
