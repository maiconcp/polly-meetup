using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TargetService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly static Dictionary<int, int> _threshold = new Dictionary<int, int>();
        private readonly static TextWriter Logger = new StringWriter();

        public TestController()
        {
            Console.SetOut(Logger);
        }

        [HttpGet("success")]
        public ActionResult Success()
        {
            return Ok("success");
        }

        [HttpGet("error/{threshold}")]
        public ActionResult ErrorAfter(int threshold)
        {
            Console.WriteLine($"Receive ErrorAfter {threshold}");

            System.Threading.Thread.Sleep(1000);

            _threshold.TryGetValue(threshold, out int retries);

            _threshold[threshold] = ++retries;

            if (retries >= threshold)
                return new ObjectResult(false) { StatusCode = 500 };

            return Ok(true);
        }

        [HttpPost]
        public ActionResult Reset()
        {
            _threshold?.Clear();

            return Ok();
        }

        [HttpGet("status")]
        public ActionResult Status()
        {
            return Ok(Logger.ToString());
        }
    }
}