using HeroesServices.Interface;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Hub
{
    public class NotifyHub : Hub<ITypedHubClient>
    {
    }
}
