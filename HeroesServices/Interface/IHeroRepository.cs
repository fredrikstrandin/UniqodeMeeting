using System.Collections.Generic;
using System.Threading.Tasks;
using HeroesWeb.Models;

namespace HeroesWeb.Repositorys
{
    public interface IHeroRepository
    {
        Task<HeroItem> CreateAsync(HeroItem item);
        Task DeleteAsync(string id);
        Task<IEnumerable<HeroItem>> GetHerosAsync(string name = null);
        Task<HeroItem> GetHeroAsync(string id);
        Task<HeroItem> UpdateAsync(HeroItem item);
    }
}