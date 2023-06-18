
using Core.Entities.Identity;

namespace Core.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
  
    }
}
