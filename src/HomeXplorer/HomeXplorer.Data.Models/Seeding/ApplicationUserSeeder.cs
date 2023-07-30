namespace HomeXplorer.Data.Models.Seeding
{
    using Microsoft.AspNetCore.Identity;

    using HomeXplorer.Data.Entities;

    public class ApplicationUserSeeder
    {
        public ICollection<ApplicationUser> GenerateInitialUsers()
        {
            var users = new List<ApplicationUser>();

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

            users.Add(adminUser);

            var agentUser = new ApplicationUser()
            {
                Id = "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                FirstName = "Initial",
                LastName = "Agent",
                Email = "agenttest@test.bg",
                NormalizedEmail = "AGENTTEST@TEST.BG",
                UserName = "agenttest@test.bg",
                NormalizedUserName = "AGENTTEST@TEST.BG"
            };

            agentUser.PasswordHash = hasher.HashPassword(agentUser, "homeXplorerAgent123!");

            users.Add(agentUser);

            var renterUser = new ApplicationUser()
            {
                Id = "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                FirstName = "Initial",
                LastName = "Renter",
                Email = "rentertest@test.bg",
                NormalizedEmail = "RENTERTEST@TEST.BG",
                UserName = "rentertest@test.bg",
                NormalizedUserName = "RENTERTEST@TEST.BG"
            };

            renterUser.PasswordHash = hasher.HashPassword(renterUser, "homeXplorerRenter123!");

            users.Add(renterUser);

            return users;
        }
    }
}
