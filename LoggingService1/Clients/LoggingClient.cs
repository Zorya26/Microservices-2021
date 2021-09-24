using Hazelcast;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoggingService1.Clients
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
                        "172.31.93.129:5701"
                    }
                }
            };

            _client = await HazelcastClientFactory.StartNewClientAsync(options);
        }

        private async Task hazelcastMap()
        {
            _map = await _client.GetMapAsync<Guid, string>("lab4-map");
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
                return "OK";
            }

            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}