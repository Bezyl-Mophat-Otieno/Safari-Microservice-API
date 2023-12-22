
using Azure.Messaging.ServiceBus;
using EmailService.Models.Dto;
using EmailService.Service;
using MailKit;
using Newtonsoft.Json;
using System.Text;
using MailService = EmailService.Service.MailService;

namespace EmailService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer

    {
        private readonly string _connstring;

        private readonly string _queuname;

        private readonly IConfiguration _configuration;
        private readonly ServiceBusProcessor _emailprocessor;
        private readonly MailService _emailservice;


        public AzureServiceBusConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            _connstring = _configuration.GetValue<string>("Azureconnectionstring");
            _queuname = _configuration.GetValue<string>("QueueandTopics:registerqueue");

            var client = new ServiceBusClient(_connstring);

            _emailprocessor = client.CreateProcessor(_queuname);
            _emailservice = new MailService(configuration);


            
        }
        public async Task Start()
        {
            _emailprocessor.ProcessMessageAsync += OnRegisterUser;
           _emailprocessor.ProcessErrorAsync +=  ErrorHandler;
            await _emailprocessor.StartProcessingAsync(); 
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }

        private async Task OnRegisterUser(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            var user = JsonConvert.DeserializeObject<UserMessageDTO>(body);

            try {
                // Sending the Mail

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2016/01/02/16/53/lion-1118467_640.jpg\" width=\"1000\" height=\"600\">");
                stringBuilder.Append("<h1> Hello " + user.Name + "</h1>");
                stringBuilder.AppendLine("<br/> Welcome to Safari Microservices ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>You can start your first adventure!!</p>");

                await _emailservice.SendMail(user , stringBuilder.ToString());
                // When Done
                await args.CompleteMessageAsync(args.Message); // we are done delete the message from the Queue
            
            } catch (Exception ex) {

                // send mail to admin
                Console.WriteLine(ex.Message);
                throw;

            
            }
        }

     

        public async Task Stop()
        {
            await _emailprocessor.StopProcessingAsync();
            await _emailprocessor.DisposeAsync();
        }
    }
}
