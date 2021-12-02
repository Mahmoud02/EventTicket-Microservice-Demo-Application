using EventTicket.Services.Payment.Messages;
using EventTicket.Services.Payment.Model;
using EventTicket.Services.Payment.Services;
using Messages;
using MessagingBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTicket.Services.Payment.Worker
{
    public class OrderPaymentConsumer : DefaultBasicConsumer
    {
        private readonly IRabbitMqManager rabbitMqManager;
        private readonly IExternalGatewayPaymentService externalGatewayPaymentService;
        private readonly ILogger logger;


        public OrderPaymentConsumer(IRabbitMqManager rabbitMqManager, IExternalGatewayPaymentService externalGatewayPaymentService, ILogger logger)
        {
            this.rabbitMqManager = rabbitMqManager;
            this.externalGatewayPaymentService = externalGatewayPaymentService;
            this.logger = logger;
        }
        public override async void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            if (properties.ContentType != RabbitMqConstants.JsonMimeType)
                throw new ArgumentException(
                    $"Can't handle content type {properties.ContentType}");

            var messageBody = body.ToArray();
            var message = Encoding.UTF8.GetString(messageBody);
            var orderPaymentRequestMessage = JsonConvert.DeserializeObject<OrderPaymentRequestMessage>(message);
            await Consume(orderPaymentRequestMessage, deliveryTag);

        }
        private async Task Consume(OrderPaymentRequestMessage orderPaymentRequestMessage , ulong deliveryTag)
        {
            //Process Pauement
            PaymentInfo paymentInfo = new PaymentInfo
            {
                CardNumber = orderPaymentRequestMessage.CardNumber,
                CardName = orderPaymentRequestMessage.CardName,
                CardExpiration = orderPaymentRequestMessage.CardExpiration,
                Total = orderPaymentRequestMessage.Total
            };

            var result = await externalGatewayPaymentService.PerformPayment(paymentInfo);

            rabbitMqManager.SendAck(deliveryTag);

            //send payment result to order service via  bus
            OrderPaymentUpdateMessage orderPaymentUpdateMessage = new()
            {
                PaymentSuccess = result,
                OrderId = orderPaymentRequestMessage.OrderId
            };

            try
            {
                 rabbitMqManager.PublishMessage(orderPaymentUpdateMessage, RabbitMqConstants.OrderPaymentUpdatedQueue, RabbitMqConstants.OrderPaymentUpdatedRoutingKey);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}: OrderPaymentConsumer received item.");
            await Task.Delay(20000);
            logger.LogDebug($"{orderPaymentRequestMessage.OrderId}:  OrderPaymentConsumer processed item.");

        }
    }
}
