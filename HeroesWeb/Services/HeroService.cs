using HeroesWeb.Models;
using HeroesWeb.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Services
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<IEnumerable<HeroItem>> GetHeros(string name = null)
        {
            return await _heroRepository.GetHeros(name);
        }

        public async Task<HeroItem> GetHero(string id)
        {
            return await _heroRepository.GetHero(id);
        }

        public async Task<HeroItem> Create(HeroItem item)
        {
            return await _heroRepository.Create(item);
        }

        public async Task Delete(string id)
        {
            await _heroRepository.Delete(id);
        }

        public async Task<HeroItem> Update(HeroItem item)
        {
            return await _heroRepository.Update(item);
        }
    }
}
