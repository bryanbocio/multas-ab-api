using Core.Entities.Identity;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });

            service.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //Add identity options here
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                    ValidIssuer = config["Token:Issuer"],
                    ValidAudience = config["Token:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                };
            });

            service.AddAuthorization();


            return service;
        }
    }
}
