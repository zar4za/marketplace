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

        [HttpGet]
        public async Task<IActionResult> CreateToken(string steamid)
        {
            var token = _userService.GetUserToken(steamid);

            var payload = new
            {
                token
            };

            return Ok(payload);
        }

        [Authorize]
        [HttpGet("check")]
        public async Task<IActionResult> Check()
        {
            return Ok(User.Identity.Name);
        }
    }
}
