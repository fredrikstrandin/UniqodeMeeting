using HeroesWeb.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    internal class MongoDBContext
    {
        private readonly MongoDbDatabaseSetting _dbStetting;
        
        protected IMongoClient Client;
        public IMongoDatabase Database { get; set; }

        public MongoDBContext(MongoDbDatabaseSetting dbStetting)
        {
            _dbStetting = dbStetting;

            Client = new MongoClient(_dbStetting.ConnectionString);
            Database = Client.GetDatabase(_dbStetting.Database);
        }

        internal IMongoCollection<HeroesEntity> HeroesEntityCollection => Database.GetCollection<HeroesEntity>("HeroesEntity");        
    }

}
