using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTicket.Services.Ordering.Messaging
{
    public interface IRabbitMqConsumerService
    {
        void Start();
        void Stop();
    }
}
