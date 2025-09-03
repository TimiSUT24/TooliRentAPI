using Microsoft.AspNetCore.Identity;
using TooliRentClassLibrary.Models;

namespace TooliRentAPI.Data
{
    public static class UserRoleSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //Seed roles
            var role = new[]
            {
                "Admin", "User"
            };

            foreach (var roleName in role)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var identityRole = new IdentityRole
                    {
                        Name = roleName
                    };
                    await roleManager.CreateAsync(identityRole);
                }
            }

            //Seed Admin User
            var adminEmail = "Admin123@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if(adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "Admin123",
                    Email = adminEmail,                  
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin"); 
            }

            //Seed Regular User
            var userEmail = "User123@gmail.com";
            var regularUser = await userManager.FindByEmailAsync(userEmail);

            if (regularUser == null)
            {
                regularUser = new ApplicationUser
                {
                    UserName = "User123",
                    Email = userEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(regularUser, "User123!");
                await userManager.AddToRoleAsync(regularUser, "User");
            }

        }
    }
}
