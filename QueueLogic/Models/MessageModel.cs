using System;

namespace QueueLogic.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }


        public override string ToString() =>
            $"{Id}: {Value}";
    }
}