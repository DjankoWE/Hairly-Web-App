using static Hairly.GCommon.ApplicationConstants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Hairly.Data.Seeding
{
    public class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { UserRoleName, HairdresserRoleName, AdminRoleName };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = await userManager.FindByEmailAsync(DefaultAdminEmail);

            if (adminUser != null)
            {
                if (!await userManager.IsInRoleAsync(adminUser, AdminRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, AdminRoleName);
                }

                if (!await userManager.IsInRoleAsync(adminUser, HairdresserRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, HairdresserRoleName);
                }
            }

            var hairdresserUser = await userManager.FindByEmailAsync(DefaultHairdresserEmail);

            if (hairdresserUser == null)
            {
                hairdresserUser = new IdentityUser
                {
                    UserName = DefaultHairdresserEmail,
                    Email = DefaultHairdresserEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(hairdresserUser, DefaultHairdresserPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hairdresserUser, HairdresserRoleName);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(hairdresserUser, HairdresserRoleName))
                {
                    await userManager.AddToRoleAsync(hairdresserUser, HairdresserRoleName);
                }
            }

            await AsignUserRoleToUsersAsync(userManager);
        }

        private static async Task AsignUserRoleToUsersAsync(UserManager<IdentityUser> userManager)
        {
            var allUsers = userManager.Users.ToList();

            foreach (var user in allUsers)
            {
                var roles = await userManager.GetRolesAsync(user);

                if (!roles.Any())
                {
                    await userManager.AddToRoleAsync(user, UserRoleName);
                }
            }
        }
    }
}
