namespace HomeXplorer
{
    using Microsoft.EntityFrameworkCore;

    using HomeXplorer.Extensions;
    using HomeXplorer.Core.Contexts;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<HomeXplorerDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.UseStatusCodePagesWithRedirects("/Home/Error?error={0}");

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            //app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();


            //app.MapControllerRoute(
            //    name: "ResetPassword",
            //    pattern: "User/ResetPassword/{userId}/{token}",
            //    defaults: new { controller = "User", action = "ResetPassword" });

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}