namespace HomeXplorer.Data.Models.Seeding
{
    using HomeXplorer.Data.Entities;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUserSeeder
    {
        public ApplicationUser GenerateAdminUser()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser()
            {
                Id = "a30c9896-54aa-4901-878a-b1bd6417f91e",
                FirstName = "Platform",
                LastName = "Admin",
                Email = "applicationtest@abv.bg",
                NormalizedEmail = "APPLICATIONTEST@ABV.BG",
                UserName = "applicationtest@abv.bg",
                NormalizedUserName = "APPLICATIONTEST@ABV.BG"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "homeXplorerAdmin123!");

            return adminUser;
        }
    }
}
