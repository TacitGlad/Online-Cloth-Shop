using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Obaju.Constants;
using Obaju.Models;
using System.Threading.Tasks;

namespace Obaju.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private const string DefaultAdminEmail = "admin@gmail.com";
        private const string DefaultAdminPassword = "admin";
        private const string DefaultUserEmail = "user@gmail.com";
        private const string DefaultUserPassword = "user";

        private static readonly IdentityRole[] defaultRoles = new IdentityRole[]
        {
            new IdentityRole(RoleName.Admin),
            new IdentityRole(RoleName.Member),
        };

        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await SeedRoles(roleManager);

                await SeedUsers(userManager, roleManager);
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in defaultRoles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task SeedUsers(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = await userManager.FindByEmailAsync(DefaultAdminEmail);
            var memberUser = await userManager.FindByEmailAsync(DefaultUserEmail);

            if (adminUser == null)
            {
                adminUser = new User()
                {
                    UserName = DefaultAdminEmail,
                    Email = DefaultAdminEmail
                };

                await userManager.CreateAsync(adminUser, DefaultAdminPassword);
            }

            if (memberUser == null)
            {
                memberUser = new User()
                {
                    UserName = DefaultUserEmail,
                    Email = DefaultUserEmail
                };

                await userManager.CreateAsync(memberUser, DefaultUserPassword);
            }

            await userManager.AddToRoleAsync(adminUser, RoleName.Admin);
            await userManager.AddToRoleAsync(memberUser, RoleName.Member);
        }
    }
}
