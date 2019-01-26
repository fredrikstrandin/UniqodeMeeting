using HeroesServices.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Hub
{
    public class HeroesHub : Hub<IHeroesHubClient>
    {
        public async Task SendNotifycation(string type, string message)
        {
            //save notifycation 

            //Send it to all client
            await  this.Clients.All.BroadcastMessage(type, message);
        }
        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
