using HeroesWeb.Models;
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
        public async Task<long> GetETagAsync(string id, string collection, string key = "Version")
        {
            var coll = _context.Database.GetCollection<BsonDocument>(collection);

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

        public async Task SetETagAsync(string collection, string key, string id, long value)
        {
            var coll = _context.Database.GetCollection<BsonDocument>(collection);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));

            var update = await coll.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id)),
                Builders<BsonDocument>.Update
                    .Set("Version", DateTime.Now.Millisecond));
        }
    }
}
