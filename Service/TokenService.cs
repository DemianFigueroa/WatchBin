using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WatchBin.Users;

namespace WatchBin.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            var signingKey =
                Environment.GetEnvironmentVariable("JWT_SIGNING_KEY")
                ?? throw new InvalidOperationException(
                    "JWT_SIGNING_KEY environment variable is missing."
                );

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

        public string CreateToken(AppUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email
                        ?? throw new ArgumentNullException(
                            nameof(user.Email),
                            "User email cannot be null."
                        )
                ),
                new Claim(
                    JwtRegisteredClaimNames.GivenName,
                    user.UserName
                        ?? throw new ArgumentNullException(
                            nameof(user.UserName),
                            "Username cannot be null."
                        )
                ),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer =
                    _config["JWT:Issuer"]
                    ?? throw new InvalidOperationException(
                        "JWT:Issuer is missing in configuration."
                    ),
                Audience =
                    _config["JWT:Audience"]
                    ?? throw new InvalidOperationException(
                        "JWT:Audience is missing in configuration."
                    ),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
