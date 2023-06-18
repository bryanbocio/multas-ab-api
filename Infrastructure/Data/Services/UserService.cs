using Core.Entities.Identity;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async  Task<bool> CreateUserAsync(AppUser user, string password, string rol)
        {
            var userCreated = await _userManager.CreateAsync(user, password);

            var rolesAvalibles = Enum.GetValues(typeof(Roles));

            await AsignRoles(rolesAvalibles,rol, user); 

            return userCreated.Succeeded;

        }


        private async Task AsignRoles(Array rolesAvalibles, string rolToAsing, AppUser appUser)
        {

            if (String.IsNullOrEmpty(rolToAsing))
            {
                await _userManager.AddToRoleAsync(appUser, Roles.USER.ToString());
            }
            else
            {
                foreach (var role in rolesAvalibles)
                {
                    if (role.Equals(rolToAsing))
                    {
                        await _userManager.AddToRoleAsync(appUser, role.ToString());
                    }
                }
            }

           
        }
    }
}
