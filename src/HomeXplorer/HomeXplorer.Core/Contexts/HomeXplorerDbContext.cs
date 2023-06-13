namespace HomeXplorer.Core.Contexts
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class HomeXplorerDbContext : IdentityDbContext
    {
        public HomeXplorerDbContext(DbContextOptions<HomeXplorerDbContext> options)
            : base(options)
        {
        }
    }
}
