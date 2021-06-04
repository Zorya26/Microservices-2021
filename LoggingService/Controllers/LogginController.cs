using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LogginController : Controller
    {
        private readonly ILogger<LogginController> _logger;

        public LogginController(ILogger<LogginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetLog() => "LogginController";

    }
}