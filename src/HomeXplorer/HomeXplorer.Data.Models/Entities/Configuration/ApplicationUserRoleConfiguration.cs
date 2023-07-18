namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserRoleConfiguration
        : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        private readonly ApplicationUserRoleSeeder seeder;

        public ApplicationUserRoleConfiguration()
        {
            this.seeder = new ApplicationUserRoleSeeder();
        }

        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            var adminUserRole = this.seeder.MapRoleToAdminUser();
            builder.HasData(adminUserRole);
        }
    }
}
