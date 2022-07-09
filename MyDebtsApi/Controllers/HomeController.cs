using Microsoft.AspNetCore.Mvc;
using MyDebtsApi.Attributes;

namespace MyDebtsApi.Controllers
{

    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        [ApiKey]
        public IActionResult Get()
        {
            return Ok("Testando Api");
        }
    }
}
