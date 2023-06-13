namespace HomeXplorer.Extensions
{
    using HomeXplorer.Core.Contexts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
                .AddEntityFrameworkStores<HomeXplorerDbContext>();
            services.AddControllersWithViews();

            return services;
        }
    }
}
