using LoggingService3.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueueLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService3.Controllers
{
    [Route("api/[controller]")]
    public class LogginController : Controller
    {
        private readonly ILogger<LogginController> _logger;
        private readonly LoggingClient _loggingClient;

        public LogginController(ILogger<LogginController> logger, LoggingClient loggingClient)
        {
            _logger = logger;
            _loggingClient = loggingClient;
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