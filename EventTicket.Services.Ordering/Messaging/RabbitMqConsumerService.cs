using EventTicket.Services.Ordering.Repositories;
using Messages;
using MessagingBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTicket.Services.Ordering.Messaging
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService
    {
        private readonly IRabbitMqManager checkoutMessageReceiverClient;
        private readonly IRabbitMqManager orderPaymentUpdateMessageReceiverClient;
        private readonly OrderRepository _orderRepository;

        public RabbitMqConsumerService(IRabbitMqManager checkoutMessageReceiverClient , IRabbitMqManager orderPaymentUpdateMessageReceiverClient, OrderRepository orderRepository) {
            this.checkoutMessageReceiverClient = checkoutMessageReceiverClient;
            this.orderPaymentUpdateMessageReceiverClient = orderPaymentUpdateMessageReceiverClient;
            _orderRepository = orderRepository;

        }
        public void Start()
        {
            //Declare Consumers: OnCheckoutMessage Received
            var checkoutMessageConsumer = new CheckoutMessageConsumer(this.checkoutMessageReceiverClient,this._orderRepository);
            checkoutMessageReceiverClient.ListenForQueue(RabbitMqConstants.CheckoutMessageQueue, checkoutMessageConsumer);

            //Declare Consumers: OnOrderUpdate Received
            var orderPaymentUpdateConsumer = new OrderPaymentUpdateConsumer(this.orderPaymentUpdateMessageReceiverClient, this._orderRepository);
            checkoutMessageReceiverClient.ListenForQueue(RabbitMqConstants.OrderPaymentUpdatedQueue, orderPaymentUpdateConsumer);
        }

        public void Stop()
        {

        }
    }
}
