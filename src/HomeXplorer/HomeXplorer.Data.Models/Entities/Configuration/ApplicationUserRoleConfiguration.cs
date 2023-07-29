namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Models.Seeding;

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
            var mappedRoles = this.seeder.MapRolesToUsers();
            builder.HasData(mappedRoles);
        }
    }
}
