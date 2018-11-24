using MongoDB.Driver;

namespace HeroesWeb.Repositorys
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; set; }
        IMongoCollection<HeroesEntity> HeroesEntityCollection { get; }
    }
}