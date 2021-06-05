using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Models;
using Shared.Utilities;

namespace FacadeService.Controllers
{
    [Route("api/[controller]")]
    public class FacadeController : Controller
    {
        private readonly ILogger<FacadeController> _logger;

        public FacadeController(ILogger<FacadeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetFacade()
        {
            var loggingData = WebUtilities.GetRequest($"https://localhost:44389/api/Loggin");
            var messagesData = WebUtilities.GetRequest($"https://localhost:44393/api/Message");

            var result = loggingData + messagesData;

            _logger.LogInformation(result);

            return result;
        }

        [HttpPost]
        public string PostFacade([FromBody] string str)
        {
            var message = new MessageModel()
            {
                Id = Guid.NewGuid(),
                Value = str
            };

            var postRequest = WebUtilities.SendPostRequest($"https://localhost:44389/api/Loggin", JsonSerializer.Serialize(message));

            _logger.LogInformation(postRequest);

            return postRequest;
        }
    }
}