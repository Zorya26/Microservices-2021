using LoggingService1.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService1.Controllers
{
    [Route("api/[controller]")]
    public class LogginController : Controller
    {
        private readonly ILogger<LogginController> _logger;
        private readonly LoggingClient _loggingClient;

        public static Dictionary<Guid, string> messages = new Dictionary<Guid, string>();

        public LogginController(ILogger<LogginController> logger, LoggingClient loggingClient)
        {
            _logger = logger;
            _loggingClient = loggingClient;
        }
            
        [HttpPost]
        public string LogPost([FromBody] MessageModel message)
        {
                messages.Add(message.Id, message.Value);

            _logger.LogInformation("Request to Logging Controller");

            return "OK";
        }


        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync()
        {
            return await _loggingClient.GetMessages();
        }

        [HttpPost]
        public async Task<string> PostAsync([FromBody] MessageModel message)
        {
            _logger.LogInformation($"Message: {message.Value}; Id: {message.Id}");

            return await _loggingClient.SetMessages(message);
        }
    }
}