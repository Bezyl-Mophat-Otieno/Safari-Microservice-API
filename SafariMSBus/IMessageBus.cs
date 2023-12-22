using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafariMSBus
{
    internal interface IMessageBus
    {

        Task PublishMessage(Object message , string Topic_Queue_Name);
    }
}
