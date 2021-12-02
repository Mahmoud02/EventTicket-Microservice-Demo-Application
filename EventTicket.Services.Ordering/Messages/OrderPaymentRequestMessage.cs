using EventTicket.MessageBroker.Messages;
using System;

namespace EventTicket.Services.Ordering.Messages
{
    public class OrderPaymentRequestMessage: BaseMessage
    {
        public Guid OrderId { get; set; }
        public int Total { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }
    }
}
