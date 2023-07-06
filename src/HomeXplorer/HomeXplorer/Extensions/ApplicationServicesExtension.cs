namespace HomeXplorer.Extensions
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity.UI.Services;

    using CloudinaryDotNet;

    using HomeXplorer.Filters;
    using HomeXplorer.Config.SMTP;
    using HomeXplorer.Config.Google;
    using HomeXplorer.Core.Contexts;
    using HomeXplorer.Data.Entities;
    using HomeXplorer.Config.Cloudinary;
    using HomeXplorer.Core.Repositories;
    using HomeXplorer.Services.Contracts;
    using HomeXplorer.Services.Exceptions;
    using HomeXplorer.Services.Interfaces;
    using HomeXplorer.Services.Exceptions.Contracts;
    using HomeXplorer.ModelBinders;

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

                //options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Strict;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(PageVisitCountFilter));
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();

            services.Configure<GoogleCaptchaSettings>(configuration.GetSection("GoogleReCaptcha"));
            services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

            Cloudinary cloudinaryInstance =
                CloudinaryConfig.GetCloudinaryInstance(configuration);

            services.AddSingleton(cloudinaryInstance);
            services.AddSingleton(typeof(CloudinaryConfig));
            services.AddSingleton<IEmailSender, SmtpEmailSender>();

            services.AddScoped<PageVisitCountFilter>();
            services.AddScoped(typeof(GoogleCaptchaConfig));
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAgentPropertyService, AgentPropertyService>();
            services.AddScoped<IBuildingTypeService, BuildingTypeService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<IPropertyStatusService, PropertyStatusService>();
            services.AddScoped<IRenterPropertyService, RenterPropertyService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IGuard, Guard>();
            services.AddScoped<IAgentPropertyService, AgentPropertyService>();

            return services;
        }
    }
}
