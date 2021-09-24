using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Shared.Models;

namespace LoggingService.Controllers
{
    [Route("api/[controller]")]
    public class LogginController : Controller
    {
        private readonly ILogger<LogginController> _logger;

        public static Dictionary<Guid, string> messages = new Dictionary<Guid, string>();

        public LogginController(ILogger<LogginController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetLog()
        {
            var messagesCont = "";

            foreach (var message in messages.Values)
            {
                messagesCont += message;
            }

            _logger.LogInformation("Request to Logging Controller");

            return messagesCont;
        }
            
        [HttpPost]
        public string LogPost([FromBody] MessageModel message)
        {
                messages.Add(message.Id, message.Value);

            _logger.LogInformation("Request to Logging Controller");

            return "OK";
        }
    }
}