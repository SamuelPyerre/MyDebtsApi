using Microsoft.AspNetCore.Mvc;

namespace MyDebtsApi.Controllers
{

    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Testando Api");
        }
    }
}
