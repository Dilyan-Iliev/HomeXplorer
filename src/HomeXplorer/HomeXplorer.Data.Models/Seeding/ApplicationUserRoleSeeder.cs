namespace HomeXplorer.Data.Models.Seeding
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUserRoleSeeder
    {
        public IdentityUserRole<string> MapRoleToAdminUser()
        {
            return new IdentityUserRole<string>() 
            {
                UserId = "a30c9896-54aa-4901-878a-b1bd6417f91e",
                RoleId = "e7c3115f-215e-4f62-a15f-ffa31b0f6ac1"
            };
        }
    }
}
