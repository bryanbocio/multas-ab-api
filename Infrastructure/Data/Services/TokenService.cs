
using Core.Entities.Identity;
using Core.Interfaces.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Data.Services
{
    public class TokenService : ITokenService
    {

        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _configuration = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.IdentityAppUser),
            };

            await AddUserRolesToClaims(claims, user);


            var credencials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credencials,
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        private async Task AddUserRolesToClaims(List<Claim> claims, AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            if (!userRoles.IsNullOrEmpty())
            {
                foreach(var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
        }
    }
}
