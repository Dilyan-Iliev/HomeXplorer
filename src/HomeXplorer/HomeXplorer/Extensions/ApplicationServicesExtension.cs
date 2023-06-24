namespace HomeXplorer.Extensions
{
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                    configuration.GetValue<bool>("Identity:RequireConfirmedAccount");

                options.User.RequireUniqueEmail =
                    configuration.GetValue<bool>("Identity:RequireUniqueEmail");

                options.SignIn.RequireConfirmedPhoneNumber =
                    configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");

                options.Password.RequireNonAlphanumeric =
                    configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");

                options.Password.RequireUppercase =
                    configuration.GetValue<bool>("Identity:RequireUppercase");

                options.Password.RequireDigit = 
                    configuration.GetValue<bool>("Identity:RequireDigit");

                options.Password.RequiredLength =
                    configuration.GetValue<int>("Identity:RequiredLength");
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HomeXplorerDbContext>();

            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/Login";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            return services;
        }
    }
}
