namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using HomeXplorer.Data.Models.Seeding;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class RoleConfiguration
        : IEntityTypeConfiguration<IdentityRole>
    {
        private readonly RoleSeeder seeder;

        public RoleConfiguration()
        {
            this.seeder = new RoleSeeder();
        }

        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
           var adminRole = this.seeder.GenerateAdminRole();
            builder.HasData(adminRole);
        }
    }
}
