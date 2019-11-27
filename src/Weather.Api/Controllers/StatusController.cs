using Microsoft.AspNetCore.Mvc;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("Running");
        }
    }
}