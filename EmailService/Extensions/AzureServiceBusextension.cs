using EmailService.Messaging;

namespace EmailService.Extensions
{
    public  static class AzureServiceBusextension
    {
        private static IAzureServiceBusConsumer _azureServiceBusConsumer;
        public static IApplicationBuilder useAzure(this IApplicationBuilder app)
        {

            // Know about the consumer app and the lifetime of the app

            _azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var HostLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            HostLifetime.ApplicationStarted.Register(OnAppStart);
            HostLifetime.ApplicationStopping.Register(OnAppStop);

            return app;


        }

        private static void OnAppStop()
        {
            _azureServiceBusConsumer.Stop();
        }

        private static void OnAppStart()
        {
            _azureServiceBusConsumer.Start();
        }
    }
}
