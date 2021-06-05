using System;

namespace Shared.Models
{
    public class MessageModel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }


        public override string ToString() =>
            $"{Id}: {Value}";
    }
}