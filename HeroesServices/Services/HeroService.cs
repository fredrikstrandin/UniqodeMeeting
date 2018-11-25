using HeroesServices.Interface;
using HeroesWeb.Models;
using HeroesWeb.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Services
{
    public class HeroesService : IHeroesService
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IETagService _ETagService;

        public HeroesService(IHeroRepository heroRepository, IETagService ETagService)
        {
            _heroRepository = heroRepository;
            _ETagService = ETagService;
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
            Task<HeroItem> task1 = _heroRepository.CreateAsync(item);
            var task2 = _ETagService.SetETagAsync("HeroesEntity", item.Id, DateTime.Now.Ticks);

            HeroItem ret = await task1;
            await task2;

            return ret;
        }

        public async Task DeleteAsync(string id)
        {
            await _heroRepository.DeleteAsync(id);
        }

        public async Task<HeroItem> UpdateAsync(HeroItem item)
        {
            var task1 = _heroRepository.UpdateAsync(item);

            var task2 = _ETagService.SetETagAsync("Heroes", item.Id, DateTime.Now.Ticks);

            HeroItem ret = await task1;
            await task2;

            return ret;
        }
    }
}
