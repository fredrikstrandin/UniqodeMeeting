using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroMongoDBReposotory.Models
{
    public class CollectionSatusEntity
    {
        [BsonId]
        public string Collection { get; set; }
        public long Version { get; set; }
    }
}
