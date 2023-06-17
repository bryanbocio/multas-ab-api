using Core.Entities.Identity;
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
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    IdentityAppUser = "40233337290",
                    Email = "appuser@multas.com",
                    UserName = "Multas User",
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }
    }
}
