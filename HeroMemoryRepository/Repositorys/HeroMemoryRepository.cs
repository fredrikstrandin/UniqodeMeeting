using HeroesWeb.Models;
using HeroMemoryRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public class HeroesMemoryRepository : IHeroRepository
    {
        private readonly IMemoryContext _memoryContext;

        public HeroesMemoryRepository(IMemoryContext memoryContext)
        {
            _memoryContext = memoryContext;
        }
        public Task<IEnumerable<HeroItem>> GetHerosAsync(string name = null)
        {
            IEnumerable<HeroItem> list = null;

            if (string.IsNullOrEmpty(name))
            {
                list = _memoryContext.Heroeslist;
            }
            else
            {
                list = _memoryContext.Heroeslist.Where(x => x.Name.Contains(name)).ToList();
            }


            return Task.FromResult<IEnumerable<HeroItem>>(list);
        }

        public Task<HeroItem> GetHeroAsync(string id)
        {
            HeroItem hero = null;

            hero = _memoryContext.Heroeslist.FirstOrDefault(x => x.Id == id);

            return Task.FromResult<HeroItem>(hero);
        }

        public Task<HeroItem> CreateAsync(HeroItem item)
        {
            if (default(HeroItem) != item)
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    item.Id = Guid.NewGuid().ToString();
                }

                if (default(int) == item.EmpNo)
                {
                    item.EmpNo = _memoryContext.Heroeslist.Max(x => x.EmpNo) + 1;
                }

                _memoryContext.Heroeslist.Add(item);
            }

            return Task.FromResult<HeroItem>(item);
        }

        public Task<HeroItem> UpdateAsync(HeroItem item)
        {
            HeroItem old = _memoryContext.Heroeslist.FirstOrDefault(x => x.Id == item.Id);
            if (default(HeroItem) != old)
            {
                _memoryContext.Heroeslist.Remove(old);
                _memoryContext.Heroeslist.Add(item);
            }

            return Task.FromResult<HeroItem>(item);
        }

        public Task DeleteAsync(string id)
        {
            HeroItem hero = null;

            hero = _memoryContext.Heroeslist.FirstOrDefault(x => x.Id == id);
            if (default(HeroItem) != hero)
            {
                _memoryContext.Heroeslist.Remove(hero);
            }

            return Task.CompletedTask;
        }
        public Task DeleteAllAsync()
        {
            _memoryContext.Heroeslist = new List<HeroItem>();
            _memoryContext.DictionaryItem = new Dictionary<string, Dictionary<string, long>>();
            _memoryContext.DictionaryList = new Dictionary<string, long>();

            return Task.CompletedTask;
        }
    }
}
