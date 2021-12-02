using EventTicket.MessageBroker.Messages;
using System;

namespace EventTicket.Services.Payment.Messages
{
    public class OrderPaymentUpdateMessage: BaseMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
