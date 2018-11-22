using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesWeb.Models
{
    [BsonIgnoreExtraElements]
    internal class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// This attribute is for Etag
        /// </summary>
        public long? Version { get; set; } = DateTime.UtcNow.Ticks;
        [BsonIgnoreIfDefault]
        public bool IsHistory { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
