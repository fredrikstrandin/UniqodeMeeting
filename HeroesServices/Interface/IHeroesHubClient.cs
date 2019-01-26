using HeroesWeb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroesServices.Interface
{
    public interface IHeroesHubClient
    {
        Task BroadcastMessage(string type, string payload);
        Task AddHeroes(HeroItem hero);
        Task UpdateHeroes(HeroItem hero);
        Task DeleteHeroes(string id);

    }
}
