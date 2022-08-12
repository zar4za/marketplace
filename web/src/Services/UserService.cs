using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Grecatech.Security.OpenID;
using System.Text.RegularExpressions;

namespace Web.Services
{
    public class UserService
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly string _audience;
        private SteamProvider _steamProvider;

        private static DateTime ExpirationTime => DateTimeOffset.UtcNow.AddMinutes(240).UtcDateTime;

        public UserService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _audience = configuration["Bearer:Audience"];
            var secret = configuration["Bearer:Password"];
            var bytes = Encoding.ASCII.GetBytes(secret);
            var key = new SymmetricSecurityKey(bytes);
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            _steamProvider = new SteamProvider(httpClientFactory.CreateClient());
        }

        public string GetRedirectLink(string verifyLink)
        {
            return OpenIdRequestBuilder.Create()
                .WithProvider("https://steamcommunity.com/openid")
                .WithSpec(OpenIdRequestBuilder.Spec.OpenId20)
                .WithMode(OpenIdRequestBuilder.Mode.CheckIdSetup)
                .WithIdentifierSelect()
                .WithReturn(verifyLink)
                .Build();
        }

        public async Task<string?> VerifyOpenId(string query)
        {
            var response = await _steamProvider.Verify(query);
            var validationRegex = new Regex(@"is_valid:[truefals]{4,5}");
            var validationMatch = validationRegex.Match(response);

            if (!validationMatch.Success || !Convert.ToBoolean(validationMatch.Value.Replace("is_valid:", "")))
                return null;

            var regex = new Regex(@"https%3A%2F%2Fsteamcommunity.com%2Fopenid%2Fid%2F[0-9]{17}");
            var match = regex.Match(query);
            return match.Value.Replace("https%3A%2F%2Fsteamcommunity.com%2Fopenid%2Fid%2F", "");
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
