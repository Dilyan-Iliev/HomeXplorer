namespace HomeXplorer.Data.Models.Entities.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using HomeXplorer.Data.Models.Seeding;

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
            var roles = this.seeder.GenerateRoles();
            builder.HasData(roles);
        }
    }
}
