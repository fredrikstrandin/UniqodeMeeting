using HeroesWeb.Models;
using HeroMongoDBReposotory.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Repositorys
{
    public class HeroMongodbRepository : IHeroRepository
    {
        private readonly IMongoDBContext _context;
        
        public HeroMongodbRepository(IMongoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HeroItem>> GetHerosAsync(string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return (await _context.HeroesEntityCollection.Find(x => x.NameNormalize.Contains(name.Normalize()))
                    .Sort(Builders<HeroesEntity>.Sort.Ascending("EmpNo"))
                    .ToListAsync())
                    .Select(x => (HeroItem)x); 
            }

            return (await _context.HeroesEntityCollection.Find(x => true)
                 .Sort(Builders<HeroesEntity>.Sort.Ascending("EmpNo"))
                 .ToListAsync())
                 .Select(x => (HeroItem)x);
        }

        public async Task<HeroItem> GetHeroAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId idEntity))
            {
                return null;
            }

            return await _context.HeroesEntityCollection
                .Find(x => x.Id == idEntity)
                .FirstOrDefaultAsync(); 
        }

        public async Task<HeroItem> CreateAsync(HeroItem item)
        {
            HeroesEntity entity = item;

            if (default(HeroesEntity) != entity)
            {
                if (ObjectId.Empty == entity.Id)
                {
                    entity.Id = ObjectId.GenerateNewId();
                    entity.EmpNo = (await _context.HeroesEntityCollection.Find(x => true)
                        .SortByDescending(a => a.EmpNo)
                        .Project(x => x.EmpNo)
                        .FirstOrDefaultAsync()) + 1;
                }

                await _context.HeroesEntityCollection.InsertOneAsync(entity);

                return entity;
            }

            return null;
        }

        public async Task<HeroItem> UpdateAsync(HeroItem item)
        {
            HeroesEntity entity = item;

            var filter = Builders<HeroesEntity>.Filter.Eq(x => x.Id, entity.Id);

            var update = Builders<HeroesEntity>.Update
                .Set(x => x.Version, DateTime.UtcNow.Ticks);

            if (!string.IsNullOrEmpty(item.Name))
            {
                update = update.Set(x => x.Name, item.Name);
            }
            
            if (!string.IsNullOrEmpty(item.City))
            {
                update = update.Set(x => x.City, item.City);
            }

            var ret = await _context.HeroesEntityCollection.UpdateOneAsync(filter, update);
            
            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId idEntity))
            {
                return;
            }

            var filter = Builders<HeroesEntity>.Filter.Eq(x => x.Id, idEntity);

            await _context.HeroesEntityCollection.DeleteOneAsync(filter);

            return;
        }
        public async Task DeleteAllAsync()
        {
            await _context.HeroesEntityCollection.DeleteManyAsync(x => true);

            return;
        }
    }
}
