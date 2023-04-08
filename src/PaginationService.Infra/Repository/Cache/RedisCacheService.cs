using Newtonsoft.Json;
using PaginationService.Domain.DataContracts.Cache;
using StackExchange.Redis;

namespace PaginationService.Infra.Repository.Cache
{

    public class RedisCacheService<T> : ICacheService<T>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _cache;

        public RedisCacheService(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _cache = _redisConnection.GetDatabase();
        }

        public async Task<T> GetAsync(string key)
        {
            var cachedData = await _cache.StringGetAsync(key);
            if (!cachedData.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<T>(cachedData);
            }
            return default(T);
        }

        public async Task SetAsync(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            await _cache.StringSetAsync(key, json);
        }
    }
}
