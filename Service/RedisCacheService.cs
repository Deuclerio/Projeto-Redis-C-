using Estudos.Redis.Api.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;

namespace Estudos.Redis.Api.Service
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            try
            {
                var value = _cache.GetString(key);

                if (value != null)
                {
                    return JsonSerializer.Deserialize<T>(value);
                }

                return default;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }

        public T Set<T>(string key, T value)
        {
            var timeOut = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                SlidingExpiration = TimeSpan.FromMinutes(60)
            };

            _cache.SetString(key, JsonSerializer.Serialize(value), timeOut);

            return value;
        }
    }
}
