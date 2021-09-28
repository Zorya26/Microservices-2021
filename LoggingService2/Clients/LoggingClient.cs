using Hazelcast;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Logging;
using QueueLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService2.Clients
{
    public class LoggingClient
    {
        private IHazelcastClient _client;
        private IHMap<Guid, string> _map;
        private readonly ILogger<LoggingClient> _logger;


        private async Task hazelcastClient()
        {
            var options = new HazelcastOptions
            {
                Networking =
                {
                    Addresses =
                    {
                        "localhost:6002"
                    }
                }
            };

            _client = await HazelcastClientFactory.StartNewClientAsync(options);
        }

        private async Task hazelcastMap()
        {
            _map = await _client.GetMapAsync<Guid, string>("lab4-my-map");
        }

        public async Task<IEnumerable<string>> GetMessages()
        {
            await hazelcastClient();
            await hazelcastMap();

            try
            {
                return await _map.GetValuesAsync();
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<string> SetMessages(MessageModel message)
        {
            await hazelcastClient();
            await hazelcastMap();

            try
            {
                await _map.SetAsync(message.Id, message.Value);
                return message.Value;
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}