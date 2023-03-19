using MongoDB.Driver;

namespace RedisMongoDemo.Data
{
    public interface IMongoDBContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}