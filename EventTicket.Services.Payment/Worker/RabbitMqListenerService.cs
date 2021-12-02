using EventTicket.Services.Payment.Services;
using Messages;
using MessagingBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EventTicket.Services.Payment.Worker
{
    public class RabbitMqListenerService : IHostedService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IExternalGatewayPaymentService externalGatewayPaymentService;
        private readonly IRabbitMqManager rabbitMqManager;

        public RabbitMqListenerService(IConfiguration configuration, ILoggerFactory loggerFactory, IExternalGatewayPaymentService externalGatewayPaymentService, IRabbitMqManager rabbitMqManager)
        {
            this.logger = loggerFactory.CreateLogger<RabbitMqListenerService>();
            this.configuration = configuration;
            this.externalGatewayPaymentService = externalGatewayPaymentService;
            this.rabbitMqManager = rabbitMqManager;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Declare Consumers: PaymentMessage Received
            var orderPaymentConsumer = new OrderPaymentConsumer(this.rabbitMqManager, this.externalGatewayPaymentService,this.logger);
            this.rabbitMqManager.ListenForQueue(RabbitMqConstants.OrderPaymentRequestQueue, orderPaymentConsumer);
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        protected void ProcessError(Exception e)
        {
            logger.LogError(e, "Error while processing queue item in RabbitMqListenerService.");
        }
    }
}
