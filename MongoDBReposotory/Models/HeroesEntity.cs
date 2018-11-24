using HeroesWeb.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HeroesWeb.Repositorys
{
    [BsonIgnoreExtraElements]
    public class HeroesEntity : BaseEntity
    {
        public int EmpNo { get; set; }
        public string Name { get; set; }
        public string NameNormalize => Name.Normalize();
        public string City { get; set; }

        public static implicit operator HeroesEntity(HeroItem item)
        {
            ObjectId.TryParse(item.Id, out ObjectId id);
            
            return new HeroesEntity()
            {
                Id = id,
                EmpNo = item.EmpNo,
                Name = item.Name,
                City = item.City
            };
        }

        public static implicit operator HeroItem(HeroesEntity item)
        {
            return new HeroItem()
            {
                Id = item.Id.ToString(),
                EmpNo = item.EmpNo,
                Name = item.Name,
                City = item.City
            };
        }
    }
}