using EventTicket.MessageBroker.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingBus
{
    public interface IRabbitMqManager
    {
         void PublishMessage(BaseMessage message, string queueName, string routingKey);
         void ListenForQueue(string queueName, DefaultBasicConsumer consumer);
         void SendAck(ulong deliveryTag);

    }
}
