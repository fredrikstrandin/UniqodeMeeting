using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesServices.Interface;
using HeroesServices.Models;
using HeroesWeb.Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HeroesWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IHubContext<HeroesHub, IHeroesHubClient> _hubContext;

        public MessageController(IHubContext<HeroesHub, IHeroesHubClient> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public string Post([FromBody]Message msg)
        {
            string retMessage = string.Empty;

            _hubContext.Clients.All.BroadcastMessage(msg.Type, msg.Payload);
            retMessage = "Success";

            return retMessage;
        }
    }
}