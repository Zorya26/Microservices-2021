using System;
using System.Collections.Generic;
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
            var random = GetRandom();

            var loggingData = WebUtilities.GetRequest(random);

            _logger.LogInformation(loggingData);

            return loggingData;
        }

        [HttpPost]
        public string PostFacade([FromBody] string str)
        {
            var random = GetRandom();

            var message = new MessageModel()
            {
                Id = Guid.NewGuid(),
                Value = str
            };

            var postRequest = WebUtilities.SendPostRequest(random, JsonSerializer.Serialize(message));

            _logger.LogInformation(postRequest);

            return postRequest;
        }

        private string GetRandom()
        {
            var random = new Random();
            var listLoggingData = new List<string> {
                $"https://localhost:44389/api/Loggin",
                $"https://localhost:44389/api/Loggin",
                $"https://localhost:44389/api/Loggin"
            };
            int index = random.Next(listLoggingData.Count);
            Console.WriteLine(listLoggingData[index]);
            return listLoggingData[index];
        }

    }
}