using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gero.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly string SERVER_IP_ADDRESS = "192.168.25.25";

        // GET: api/v1/servers/availability
        [HttpGet("availability")]
        public async Task<IActionResult> GetAvailability()
        {
            var ping = new Ping();

            try
            {
                var reply = ping.Send(SERVER_IP_ADDRESS);

                return Ok(new { message = "Server is available" });
            }
            catch (PingException)
            {
                return StatusCode(503, new { error = "Server is unavailable" });
            }
        }
    }
}