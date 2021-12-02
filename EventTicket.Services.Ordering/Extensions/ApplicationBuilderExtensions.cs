using EventTicket.Services.Ordering.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventTicket.Services.Ordering.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IRabbitMqConsumerService Consumer { get; set; }

        public static IApplicationBuilder UseRabbitMqConsumerService(this IApplicationBuilder app)
        {
            Consumer = app.ApplicationServices.GetService<IRabbitMqConsumerService>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            Consumer.Start();
        }

        private static void OnStopping()
        {
            Consumer.Stop();
        }
    }
}
