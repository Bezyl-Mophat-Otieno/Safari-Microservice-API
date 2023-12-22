using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SafariMSBus
{
    public class MessageBus : IMessageBus
    {
        private readonly string _connstring = "Endpoint=sb://safarimsbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=CgM4tD1vPs3lyB8pU+Yqfe2AizAWnmVIp+ASbOrf0E4=";
        public async Task PublishMessage(object message, string Topic_Queue_Name)
        {
            // Creating a client 

            var client = new ServiceBusClient(_connstring);

            ServiceBusSender sender = client.CreateSender(Topic_Queue_Name);    
            
            //Convert to JSON 

            var messagebody = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalmessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(messagebody)){ 
            
            CorrelationId = Guid.NewGuid().ToString() 
            
            };

            // sending the message

            await sender.SendMessageAsync(finalmessage);


            // Free up resources by performing a cleanup

            await sender.DisposeAsync();

        }
    }
}
