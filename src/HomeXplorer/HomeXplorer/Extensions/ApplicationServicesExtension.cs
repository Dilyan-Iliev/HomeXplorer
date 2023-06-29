namespace HomeXplorer.Extensions
{
    using CloudinaryDotNet;
    using HomeXplorer.Config.Cloudinary;
    using HomeXplorer.Config.Google;
    using HomeXplorer.Config.SMTP;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
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

            services.Configure<GoogleCaptchaSettings>(configuration.GetSection("GoogleReCaptcha"));
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            Cloudinary cloudinaryInstance =
                CloudinaryConfig.GetCloudinaryInstance(configuration);

            services.AddSingleton(cloudinaryInstance);
            services.AddSingleton(typeof(CloudinaryConfig));
            services.AddSingleton<IEmailSender, SmtpEmailSender>();
            services.AddScoped(typeof(GoogleCaptchaConfig));
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IBuildingTypeService, BuildingTypeService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            return services;
        }
    }
}
