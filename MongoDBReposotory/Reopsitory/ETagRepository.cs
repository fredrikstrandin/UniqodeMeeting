using HeroesWeb.Models;
using HeroMongoDBReposotory.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public class ETagRepository : IETagRepository
    {
        private IMongoDBContext _context;

        public ETagRepository(IMongoDBContext context)
        {
            _context = context;
        }

        public Task DeleteETagAsync(string list, string id)
        {
            //No need the entety vill be deletet any way.

            return Task.CompletedTask;
        }

        public async Task DeleteETagAsync(string list)
        {
            var filter = Builders<CollectionSatusEntity>.Filter.Eq(x => x.Collection, list);

            await _context.CollectionSatusEntityCollection.DeleteOneAsync(filter);
        }

        public async Task<long> GetETagItemAsync(string list, string key, string id)
        {
            var coll = _context.Database.GetCollection<BsonDocument>(list);

            var projection = Builders<long>.Projection.Include(key).Exclude("_id");

            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var versionProjection = Builders<BsonDocument>.Projection
                                        .Include(key)
                                        .Exclude("_id");

            var query = coll.Find(Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id)))
                .Project<BsonDocument>(versionProjection);

            var o = await query.FirstOrDefaultAsync();

            if (o != null)
            {
                return o[key].AsInt64;
            }
            else
            {
                return 0;
            }
        }

        public async Task<long> GetETagListAsync(string list)
        {
            var versionProjection = Builders<CollectionSatusEntity>.Projection
                                        .Include(x => x.Version)
                                        .Exclude("_id");

            var query = _context.CollectionSatusEntityCollection.Find(x => x.Collection == list)
                .Project<CollectionSatusEntity>(versionProjection);

            var o = await query.FirstOrDefaultAsync();

            if (o != null)
            {
                return o.Version;
            }
            else
            {
                return 0;
            }
        }

        public Task SetETagItemAsync(string collection, string id, long value)
        {
            //var coll = _context.Database.GetCollection<BsonDocument>(collection);

            //var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            //var update = await coll.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id)),
            //    Builders<BsonDocument>.Update
            //        .Set("Version", value));

            return Task.CompletedTask;
        }

        public async Task SetETagListAsync(string collection, long value)
        {
            var update = await _context.CollectionSatusEntityCollection.UpdateOneAsync(
                Builders<CollectionSatusEntity>.Filter.Eq(x => x.Collection, collection),
                Builders<CollectionSatusEntity>.Update
                    .Set("Version", value));

            if(update.MatchedCount == 0)
            {
                await _context.CollectionSatusEntityCollection.InsertOneAsync(new CollectionSatusEntity() { Collection = collection, Version = value });
            }
        }
    }
}
