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
        //private readonly IMemoryCache _memoryCache;

        public static Dictionary<Guid, string> messages = new Dictionary<Guid, string>();

        public LogginController(ILogger<LogginController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            //_memoryCache = memoryCache;
        }

        [HttpGet]
        public string GetLog()
        {
            var messagesCont = "";

            //_memoryCache.TryGetValue("messages", out messages);

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
            //if (_memoryCache.TryGetValue("messages", out messages))
 
//if (messages.Values.Count != 0)
            //{
                messages.Add(message.Id, message.Value);
            //}
            

            //else
            //{
            //    messages = new Dictionary<Guid, string>
            //    {
            //        { message.Id, message.Value }
            //    };
            //}
            
            //_memoryCache.Set("messages", messages);

            _logger.LogInformation("Request to Logging Controller");

            return "OK";
        }
    }
}