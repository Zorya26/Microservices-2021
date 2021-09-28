using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QueueLogic.Models;
using Shared.Utilities;
//using Newtonsoft.Json;
using QueueLogic;

namespace FacadeService.Controllers
{
    [Route("api/[controller]")]
    public class FacadeController : Controller
    {
        private readonly ILogger<FacadeController> _logger;
        private readonly IQueueSender _queueSender;

        public FacadeController(ILogger<FacadeController> logger, IQueueSender sender)
        {
            _logger = logger;
            _queueSender = sender;
        }

        [HttpGet]
        public string GetFacade()
        {
            var randomLogging = GetRandom(listLoggingData);
            var randomMessages = GetRandom(listMessagesData);

            var loggingData = WebUtilities.GetRequest(randomLogging);
            var messagesData = WebUtilities.GetRequest(randomMessages); //messages list

            var output = $"Logging Data: {loggingData}; Messages Data: {messagesData}";

            _logger.LogInformation(output);

            return output;
        }

        [HttpPost]
        public string PostFacade([FromBody] string str)
        {
            var random = GetRandom(listLoggingData);

            var message = new MessageModel()
            {
                Id = Guid.NewGuid(),
                Value = str
            };

            var postRequest = WebUtilities.SendPostRequest(random, JsonSerializer.Serialize(message));

            _logger.LogInformation(postRequest);
            _queueSender.SendMessage(message);

            return postRequest;
        }

        private string GetRandom(List<string> data)
        {
            var random = new Random();
            int index = random.Next(data.Count);
            Console.WriteLine(data[index]);
            return data[index];
        }

        private readonly List<string> listLoggingData = new List<string>
        {
            $"https://localhost:44389/api/Loggin",
            $"https://localhost:44390/api/Loggin",
            $"https://localhost:44391/api/Loggin"
        };

        private readonly List<string> listMessagesData = new List<string>
        {
            $"http://localhost:44392/api/message",
            $"http://localhost:44393/api/message"
        };

    }
}