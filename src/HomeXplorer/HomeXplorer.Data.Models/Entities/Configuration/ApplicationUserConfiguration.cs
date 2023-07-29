namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Entities;
    using HomeXplorer.Data.Models.Seeding;

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
            var users = this.seeder.GenerateInitialUsers();
            builder.HasData(users);
        }
    }
}
