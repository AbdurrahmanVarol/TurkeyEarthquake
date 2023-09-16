using Microsoft.Extensions.Caching.Memory;
using TurkeyEarthquake.API.Caching.Abstract;

namespace TurkeyEarthquake.API.Caching.Concrete.InMemory
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T? Get<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out object? value))
            {
                return (T?)value;
            }
            return null;
        }
        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan expiration)
        {
            _cache.Set(key, value, expiration);
        }
    }
}
