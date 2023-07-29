namespace HomeXplorer.Data.Models.Seeding
{
    using Microsoft.AspNetCore.Identity;

    public class RoleSeeder
    {
        public ICollection<IdentityRole> GenerateRoles()
        {
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "f5d5297a-1cdd-4236-ae81-bf1e7ff0f75e"
                },

                new IdentityRole()
                {
                    Id = "1fec1601-56ea-4757-ae65-590e0007a356",
                    Name = "Agent",
                    NormalizedName = "AGENT",
                    ConcurrencyStamp = "cb14d5e0-d3f3-41ef-8ff5-c13c7b030c30"
                },

                new IdentityRole()
                {
                    Id = "66a2871f-9cc2-4ece-93b9-8ec584db7ed1",
                    Name = "Renter",
                    NormalizedName = "Renter",
                    ConcurrencyStamp = "17987d43-6434-48ee-a2d9-3dafa661aa41"
                }
            };

            return roles;
        }
    }
}
