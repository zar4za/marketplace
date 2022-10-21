using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("openid-request")]
        public IActionResult RequestOpenId(string verifyLink)
        {
            var link = _userService.GetRedirectLink(verifyLink);
            var payload = new
            {
                link
            };
            return Ok(payload);
        }

        [HttpPost("openid-verify")]
        public async Task<IActionResult> VerifyOpenId([FromBody] string query)
        {
            var steamid = await _userService.VerifyOpenId(query);

            if (steamid == null) return Unauthorized();

            var jwt = _userService.GetUserToken(steamid);

            return Ok(new { jwt });
        }
    }
}
