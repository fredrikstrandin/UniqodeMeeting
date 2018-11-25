using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public interface IETagRepository
    {
        Task<long> GetETagItemAsync(string list, string key, string id);
        Task<long> GetETagListAsync(string list);
        Task SetETagItemAsync(string list, string id, long value);
        Task SetETagListAsync(string list, long value);
        
    }
}