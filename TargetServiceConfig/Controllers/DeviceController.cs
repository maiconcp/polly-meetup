using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TargetServiceConfig.Data;

namespace TargetServiceConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    { 
        [HttpPost("on")]
        public IActionResult On()
        {
            Device.Instance.On();
            return Ok();
        }

        [HttpPost("on/{timeout}")]
        public IActionResult OnWithTimeout(int timeout)
        {
            Device.Instance.On(timeout);

            return Ok();
        }

        [HttpPost("off")]
        public IActionResult Off()
        {
            Device.Instance.Off();
            return Ok();
        }

        [HttpPost("off/{timeout}")]
        public IActionResult OffWithTimeout(int timeout)
        {
            Device.Instance.Off(timeout);
            return Ok();
        }
    }
}