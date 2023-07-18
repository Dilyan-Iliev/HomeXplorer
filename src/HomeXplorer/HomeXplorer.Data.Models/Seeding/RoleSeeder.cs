namespace HomeXplorer.Data.Models.Seeding
{
    using Microsoft.AspNetCore.Identity;

    public class RoleSeeder
    {
        public IdentityRole GenerateAdminRole()
        {
            return new IdentityRole()
            {
                Id = "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "f5d5297a-1cdd-4236-ae81-bf1e7ff0f75e"
            };
        }
    }
}
