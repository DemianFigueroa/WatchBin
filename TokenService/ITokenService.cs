using WatchBin.Users;

namespace WatchBin.TokenService
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}