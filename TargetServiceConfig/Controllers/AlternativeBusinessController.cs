using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TargetServiceConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlternativeBusinessController : ControllerBase
    {
        // GET api/business
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new BusinessData(result: true));
        }
    }
}