using Microsoft.AspNetCore.Mvc;

namespace GameAPIServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello, World!");
        }
    }
}