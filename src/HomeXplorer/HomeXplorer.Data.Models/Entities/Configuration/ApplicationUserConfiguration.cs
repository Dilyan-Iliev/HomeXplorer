namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration
        : IEntityTypeConfiguration<ApplicationUser>
    {
        private readonly ApplicationUserSeeder seeder;

        public ApplicationUserConfiguration()
        {
            this.seeder = new ApplicationUserSeeder();
        }

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var users = this.seeder.GenerateAdminUser();
            builder.HasData(users);
        }
    }
}
