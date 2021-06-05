using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Utilities;
using Shared.Models;

namespace LoggingService.Controllers
{
    [Route("api/[controller]")]
    public class LogginController : Controller
    {
        private readonly ILogger<LogginController> _logger;

        public LogginController(ILogger<LogginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<string>> GetLog()
        {
            var utilities = new Utilities();
            return utilities.Get();
        }
            
        [HttpPost]
        public Task Log(MessageModel msg)
        {
            var utilities = new Utilities();
            _logger.LogInformation(msg.ToString());
            return utilities.Insert(msg);
        }
    }
}