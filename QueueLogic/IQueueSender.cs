using QueueLogic.Models;

namespace QueueLogic
{
    public interface IQueueSender
    {
        void SendMessage(MessageModel message);
    }
}
