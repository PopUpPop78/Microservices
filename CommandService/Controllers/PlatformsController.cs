using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{    
    [Route("api/command/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
            
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("Inbound post at commands service");

            return Ok("Inbound test ok from platforms controller");
        }
    }
}