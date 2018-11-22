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

        public async Task<IEnumerable<HeroItem>> GetHerosAsync(string name = null)
        {
            return await _heroRepository.GetHerosAsync(name);
        }

        public async Task<HeroItem> GetHeroAsync(string id)
        {
            return await _heroRepository.GetHeroAsync(id);
        }

        public async Task<HeroItem> CreateAsync(HeroItem item)
        {
            return await _heroRepository.CreateAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
            await _heroRepository.DeleteAsync(id);
        }

        public async Task<HeroItem> UpdateAsync(HeroItem item)
        {
            return await _heroRepository.UpdateAsync(item);
        }
    }
}
