using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MessagesService2.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMemoryCache _cache;

        Dictionary<Guid, string> messages = new Dictionary<Guid, string>();

        public MessageController(ILogger<MessageController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _cache = memoryCache;
        }

        [HttpGet]
        public string GetMessage()
        {
            var messagesCont = "";
            if (_cache.TryGetValue("messages", out messages))
            {
                foreach (var message in messages.Values)
                {
                    messagesCont += message;
                }
            }

            _logger.LogInformation("Message Controller 1: " + messagesCont);
            return messagesCont;
        }
    }
}