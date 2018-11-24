using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public interface IETagRepository
    {
        Task<long> GetETagAsync(string collection, string key, string id);
        Task SetETagAsync(string collection, string key, string id, long value);
    }
}