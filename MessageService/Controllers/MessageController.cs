using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessagesService.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetMessage()
        {
            _logger.LogInformation("Request to Message Controller");
            return "Message Controller";
        }
    }
}