using HeroMongoDBReposotory.Models;
using MongoDB.Driver;

namespace HeroesWeb.Repositorys
{
    public interface IMongoDBContext
    {
        IMongoDatabase Database { get; set; }
        IMongoCollection<HeroesEntity> HeroesEntityCollection { get; }
        IMongoCollection<CollectionSatusEntity> CollectionSatusEntityCollection { get; }
    }
}