using EventTicket.Services.Ordering.Entities;
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
    public class CheckoutMessageConsumer : DefaultBasicConsumer
    {
        private readonly IRabbitMqManager rabbitMqManager;
        private readonly OrderRepository _orderRepository;


        public CheckoutMessageConsumer(IRabbitMqManager rabbitMqManager, OrderRepository orderRepository)
        {
            this.rabbitMqManager = rabbitMqManager;
            this._orderRepository = orderRepository;
        }


        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag,bool redelivered, string exchange, string routingKey,IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            if (properties.ContentType != RabbitMqConstants.JsonMimeType)
                throw new ArgumentException(
                    $"Can't handle content type {properties.ContentType}");

            var messageBody = body.ToArray();
            var message = Encoding.UTF8.GetString(messageBody);
            var basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(message);
            await Consume(basketCheckoutMessage);
            rabbitMqManager.SendAck(deliveryTag);
        }
        private async Task Consume(BasketCheckoutMessage basketCheckoutMessage)
        {
            //save order with status not paid
            Guid orderId = Guid.NewGuid();

            Order order = new()
            {
                UserId = basketCheckoutMessage.UserId,
                Id = orderId,
                OrderPaid = false,
                OrderPlaced = DateTime.Now,
                OrderTotal = basketCheckoutMessage.BasketTotal
            };

            await _orderRepository.AddOrder(order);

            //send order payment request message
            OrderPaymentRequestMessage orderPaymentRequestMessage = new OrderPaymentRequestMessage
            {
                CardExpiration = basketCheckoutMessage.CardExpiration,
                CardName = basketCheckoutMessage.CardName,
                CardNumber = basketCheckoutMessage.CardNumber,
                OrderId = orderId,
                Total = basketCheckoutMessage.BasketTotal
            };

            try
            {
                rabbitMqManager.PublishMessage(orderPaymentRequestMessage, RabbitMqConstants.OrderPaymentRequestQueue, RabbitMqConstants.OrderPaymentRequestRoutingKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
