using Hairly.Data;
using Hairly.GCommon;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hairly.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IUserAppointmentService, UserAppointmentService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    ConfigureDefaultIdentityOptions(options);
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Error/403";
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseExceptionHandler("/Error");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await Data.Seeding.IdentitySeeder.SeedRolesAndAdminAsync(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            app.Run();
        }

        private static void ConfigureDefaultIdentityOptions(IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;

            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 3;

            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

        }
    }
}
