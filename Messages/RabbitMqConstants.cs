using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri =
            "amqp://guest:guest@localhost:5672/";
        public const string JsonMimeType =
            "application/json";

       ///Exchange
       public const string DefaultExchange = 
            "ticket.event.exchange";
        ///Queues
        public const string CheckoutMessageQueue = "CheckoutMessageQueue";
        public const string OrderPaymentRequestQueue = "OrderPaymentRequestQueue";
        public const string OrderPaymentUpdatedQueue = "OrderPaymentUpdatedQueue";
        ///Routing Keys
        public const string CheckoutMessageRoutingKey = "CheckoutMessage";
        public const string OrderPaymentRequestRoutingKey = "OrderPaymentRequest";
        public const string OrderPaymentUpdatedRoutingKey = "OrderPaymentUpdated";

        
    }
}
