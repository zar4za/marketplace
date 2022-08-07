using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello, wolrd!";
        }
    }
}
