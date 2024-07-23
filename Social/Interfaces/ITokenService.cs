using Social.Models;

namespace Social.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);

    }
}
