using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var claims = new List<Claim>
            {
                new Claim("steamid", "123")
            };


            var jwt = new JwtSecurityToken(
                    claims: claims,
                    audience: "localhost",
                    expires: DateTimeOffset.UtcNow.AddMinutes(240).UtcDateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("supersecretpassword")), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(encodedJwt);
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            return Ok(new { message = "success" });
        }
    }
}
