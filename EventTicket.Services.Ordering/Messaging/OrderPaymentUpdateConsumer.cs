using EventTicket.Services.Ordering.Messages;
using EventTicket.Services.Ordering.Repositories;
using Messages;
using MessagingBus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicket.Services.Ordering.Messaging
{
    public class OrderPaymentUpdateConsumer : DefaultBasicConsumer
    {
        private readonly IRabbitMqManager rabbitMqManager;
        private readonly OrderRepository _orderRepository;


        public OrderPaymentUpdateConsumer(IRabbitMqManager rabbitMqManager, OrderRepository orderRepository)
        {
            this.rabbitMqManager = rabbitMqManager;
            this._orderRepository = orderRepository;
        }
        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            if (properties.ContentType != RabbitMqConstants.JsonMimeType)
                throw new ArgumentException(
                    $"Can't handle content type {properties.ContentType}");

            var messageBody = body.ToArray();
            var message = Encoding.UTF8.GetString(messageBody);
            var orderPaymentUpdateMessage =JsonConvert.DeserializeObject<OrderPaymentUpdateMessage>(message);
            await Consume(orderPaymentUpdateMessage);
            rabbitMqManager.SendAck(deliveryTag);
        }
        private async Task Consume(OrderPaymentUpdateMessage orderPaymentUpdateMessage)
        {
            //save order update
            await _orderRepository.UpdateOrderPaymentStatus(orderPaymentUpdateMessage.OrderId, orderPaymentUpdateMessage.PaymentSuccess);

        }
    }
}
