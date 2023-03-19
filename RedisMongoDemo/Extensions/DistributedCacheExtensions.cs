using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace RedisMongoDemo.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(5);
            options.SlidingExpiration = unusedExpireTime;

            var json = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, json, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var json = await cache.GetStringAsync(recordId);
            if (json == null) return default;

            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
