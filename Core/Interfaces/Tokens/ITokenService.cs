
using Core.Entities.Identity;

namespace Core.Interfaces.Tokens
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
  
    }
}
