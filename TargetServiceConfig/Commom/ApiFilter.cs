using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TargetServiceConfig.Services;

namespace TargetServiceConfig.Commom
{
    public class ApiFilter : IActionFilter
    {
        private readonly Logger _logger;
        public ApiFilter(Logger logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            string data = string.Empty;
            if (context.Exception != null)
            {
                data = context.Exception.ToString();
            }
            else if (context.HttpContext.Response.Body.CanRead)
            {
                var reader = new StreamReader(context.HttpContext.Response.Body);
                data = reader.ReadToEnd();
            }

            if (context.Result is ObjectResult)
            {
                var objResult = (context.Result as ObjectResult);
                if ((objResult.Value != null) && (string.IsNullOrEmpty(data)))
                {
                    try
                    {
                        data = Newtonsoft.Json.JsonConvert.SerializeObject(objResult.Value);
                    }
                    catch { }
                }
                _logger.Add($"Out {context.HttpContext.Request.Method.ToString()} {context.HttpContext.Request.Path} Status: {(context.Result as ObjectResult).StatusCode} - {data}");
            }
            else
                _logger.Add($"Out {context.HttpContext.Request.Method.ToString()} {context.HttpContext.Request.Path} - {data}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Add($"In  {context.HttpContext.Request.Method.ToString()} {context.HttpContext.Request.Path}");
        }
    }
}
