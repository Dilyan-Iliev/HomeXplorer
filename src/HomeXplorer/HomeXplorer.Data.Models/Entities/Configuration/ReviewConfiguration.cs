namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Entities;

    //Add this configuration later when i create admin role
    //public class ReviewConfiguration
    //    : IEntityTypeConfiguration<Review>
    //{
    //    public void Configure(EntityTypeBuilder<Review> builder)
    //    {
    //        builder
    //            .HasOne(r => r.ReviewCreator)
    //            .WithMany(r => r.Reviews)
    //            .HasForeignKey(r => r.ReviewCreatorrId)
    //            .OnDelete(DeleteBehavior.Restrict);

    //        builder
    //            .HasOne(r => r.Approver)
    //            .WithMany()
    //            .HasForeignKey(r => r.ApproverId)
    //            .OnDelete(DeleteBehavior.SetNull);
    //    }
    //}
}
