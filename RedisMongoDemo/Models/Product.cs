using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RedisMongoDemo.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
    }
}