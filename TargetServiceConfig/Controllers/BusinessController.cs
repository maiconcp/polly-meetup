﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TargetServiceConfig.Data;
using TargetServiceConfig.Services;

namespace TargetServiceConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly Logger _logger;
        public BusinessController(Logger logger)
        {
            _logger = logger;
        }

        // GET api/business
        [HttpGet]
        public IActionResult Get()
        {
            Waiting();

            if (!Device.Instance.Enabled)
            {
                _logger.Add("Server is down");
                return BadRequest(new BusinessData(result: false));
            }
            return Ok(new BusinessData(result: true));
        }

        private void Waiting()
        {
            if (Device.Instance.Timeout == 0)
                return;

            _logger.Add($"Processing request... {(int)(Device.Instance.Timeout / 1000)}s...");

            System.Threading.Thread.Sleep(Device.Instance.Timeout);
        }
    }

    public class BusinessData
    {
        public BusinessData(bool result)
        {
            Result = result;
            When = DateTime.Now;
        }

        public bool Result { get; private set; }
        public DateTime When { get; private set; }
    }
}
