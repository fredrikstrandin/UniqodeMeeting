using HeroesWeb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public class MongoDBContext : IMongoDBContext
    {
        public readonly MongoDbDatabaseSetting _dbStetting;

        public IMongoClient Client;
        public IMongoDatabase Database { get; set; }

        public MongoDBContext(IOptions<MongoDbDatabaseSetting> _dbStetting)
        {
            Client = new MongoClient(_dbStetting.Value.ConnectionString);
            Database = Client.GetDatabase(_dbStetting.Value.Database);
        }

        public IMongoCollection<HeroesEntity> HeroesEntityCollection => Database.GetCollection<HeroesEntity>("HeroesEntity");        
    }
}
