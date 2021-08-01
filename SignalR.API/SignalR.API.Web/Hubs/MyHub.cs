using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.API.Web.Hubs
{
    //Api de Client arasında oluğu gibi MVC projemizde de socket örneğini gerçekleştirebiliriz
    public class MyHub:Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
