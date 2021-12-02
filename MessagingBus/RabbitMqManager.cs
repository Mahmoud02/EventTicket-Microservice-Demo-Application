using EventTicket.MessageBroker.Messages;
using Messages;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingBus
{
    public class RabbitMqManager : IDisposable,IRabbitMqManager
    {
        private readonly IModel channel;

        public RabbitMqManager()
        {
            var connectionFactory = new ConnectionFactory { Uri = new Uri(RabbitMqConstants.RabbitMqUri) };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void PublishMessage(BaseMessage message,string queueName, string routingKey)
        {
            #region queue 
            channel.ExchangeDeclare(
                exchange: RabbitMqConstants.DefaultExchange,
                type: ExchangeType.Direct
                );

            channel.QueueDeclare(
                queue: queueName,
                durable: false, 
                exclusive: false,
                autoDelete: false, 
                arguments: null);

            channel.QueueBind(
                queue: queueName,
                exchange: RabbitMqConstants.DefaultExchange,
                routingKey: routingKey);

            var serializedCommand = JsonConvert.SerializeObject(message);

            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = RabbitMqConstants.JsonMimeType;

            channel.BasicPublish(
                exchange: RabbitMqConstants.DefaultExchange,
                routingKey: routingKey,
                basicProperties: messageProperties,
                body: Encoding.UTF8.GetBytes(serializedCommand));
            #endregion
            
            Console.WriteLine($"Sent message to "+ queueName);

        }

        public void ListenForQueue(string queueName, DefaultBasicConsumer consumer)
        {
            channel.QueueDeclare(
                queue: queueName,
                durable: false, 
                exclusive: false,
                autoDelete: false, 
                arguments: null
                );

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1,global: false);

            channel.BasicConsume(
                queue: queueName,
                consumer: consumer);
        }
        public void SendAck(ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag: deliveryTag, multiple: false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        public void Dispose()
        {
            if (!channel.IsClosed)
                channel.Close();
        }
    }
}
