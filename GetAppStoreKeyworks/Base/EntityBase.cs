using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime;

namespace GetAppStoreKeyworks.Base
{
    public class EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
