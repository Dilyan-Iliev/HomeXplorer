namespace HomeXplorer.Data.Models.Seeding
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUserRoleSeeder
    {
        public ICollection<IdentityUserRole<string>> MapRolesToUsers()
        {
            var roles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = "a30c9896-54aa-4901-878a-b1bd6417f91e",
                    RoleId = "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1"
                },

                new IdentityUserRole<string>()
                {
                    UserId = "6ea2b1f0-3183-4fe5-b2fa-83b765e18e55",
                    RoleId = "1fec1601-56ea-4757-ae65-590e0007a356"
                },

                new IdentityUserRole<string>()
                {
                    UserId = "fad56a17-221a-409c-b9aa-5fa0f274f9c0",
                    RoleId = "66a2871f-9cc2-4ece-93b9-8ec584db7ed1"
                }
            };

            return roles;
        }
    }
}
