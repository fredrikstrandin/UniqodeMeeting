using System.Collections.Generic;
using System.Threading.Tasks;
using HeroesWeb.Models;

namespace HeroesWeb.Services
{
    public interface IHeroService
    {
        Task<HeroItem> Create(HeroItem item);
        Task Delete(string id);
        Task<IEnumerable<HeroItem>> GetHeros(string name = null);
        Task<HeroItem> GetHero(string id);
        Task<HeroItem> Update(HeroItem item);
    }
}