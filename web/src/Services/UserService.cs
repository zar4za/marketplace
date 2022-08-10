using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Services
{
    public class UserService
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly string _audience;

        private static DateTime ExpirationTime => DateTimeOffset.UtcNow.AddMinutes(240).UtcDateTime;

        public UserService(IConfiguration configuration)
        {
            _audience = configuration["Bearer:Audience"];
            var secret = configuration["Bearer:Password"];
            var bytes = Encoding.ASCII.GetBytes(secret);
            var key = new SymmetricSecurityKey(bytes);
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public string GetUserToken(string steamid)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, steamid)
            };


            var jwt = new JwtSecurityToken(
                    claims: claims,
                    audience: _audience,
                    expires: ExpirationTime,
                    signingCredentials: _signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
