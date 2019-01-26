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

        public async Task<HeroItem> GetHeroEmpNoAsync(int empNo)
        {
            return await _heroRepository.GetHeroEmpNoAsync(empNo);
        }
        public async Task<HeroItem> CreateAsync(HeroItem item)
        {
            HeroItem ret = await _heroRepository.CreateAsync(item);
            await  _ETagService.SetETagAsync("HeroesEntity", ret.Id, DateTime.Now.Ticks);

            return ret;
        }

        public async Task DeleteAsync(string id)
        {
            await _heroRepository.DeleteAsync(id);
            var task2 = _ETagService.DeleteETagAsync("HeroesEntity", id);

        }

        public async Task DeleteAllAsyc()
        {
            var task1 = _heroRepository.DeleteAllAsync();
            var task2 = _ETagService.DeleteETagAsync("HeroesEntity");

            await task1;
            await task2;
        }

        public async Task<HeroItem> UpdateAsync(HeroItem item)
        {
            var task1 = _heroRepository.UpdateAsync(item);

            var task2 = _ETagService.SetETagAsync("HeroesEntity", item.Id, DateTime.Now.Ticks);

            HeroItem ret = await task1;
            await task2;

            return ret;
        }
    }
}
