using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace Shared.Tools
{
    public class Manager
    {
        private readonly ConcurrentDictionary<Guid, string> _messages;
        public Manager()
        {
            _messages = new ConcurrentDictionary<Guid, string>();
        }

        public Task<IEnumerable<string>> Get() =>
            Task.FromResult(_messages.Values.AsEnumerable());

        public Task Insert(MessageModel msg)
        {
            _messages.TryAdd(msg.Id, msg.Value);
            return Task.CompletedTask;
        }
    }
}