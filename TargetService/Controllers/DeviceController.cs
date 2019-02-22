using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TargetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        public static bool Enabled { get; set; }
        public static int TimeOut { get; set; }
        [HttpPost("on")]
        public IActionResult On()
        {
            Enabled = true;
            return Ok();
        }

        [HttpPost("off")]
        public IActionResult Off()
        {
            Enabled = false;
            return Ok();
        }

        [HttpPost("timeout/{timeout}")]
        public IActionResult Timeout(int timeout)
        {
            TimeOut = timeout;
            return Ok();
        }
    }
}