using Core.Entities.Identity;
using Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.USER.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.AGENT.ToString()));
            }

            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    IdentityAppUser = "40233337290",
                    Email = "appuser@multas.com",
                    UserName = "MultasUser",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, Roles.ADMIN.ToString());
                await userManager.AddToRoleAsync(user, Roles.USER.ToString());
                await userManager.AddToRoleAsync(user, Roles.AGENT.ToString());


                 var agent = new AppUser()
                {
                    IdentityAppUser = "302111172990",
                    Email = "josermartinez@gmail.com",
                    UserName = "Jose",
                };

                await userManager.CreateAsync(agent, "Pa$$w0rd");
                await userManager.AddToRoleAsync(agent, Roles.AGENT.ToString());

                var driver = new AppUser()
                {
                    IdentityAppUser = "40244447290",
                    Email = "armandoparedes@gmail.com",
                    UserName = "Armando",
                };

                await userManager.CreateAsync(driver, "Pa$$w0rd");
                await userManager.AddToRoleAsync(driver, Roles.USER.ToString());



            }
        }
    }
}
