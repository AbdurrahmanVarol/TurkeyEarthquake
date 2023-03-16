using Microsoft.Extensions.Caching.Memory;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Caching.Concrate.Extensions;

namespace TurkeyEarthquake.API.Caching.Concrate.InMemory
{
    public class InMemoryCache : ICache
    {
        private readonly IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key) where T : class
        {
            object value;

            if (_cache.TryGetValue(key, out value))
            {
                return value.ToString().ToObject<T>();
            }
            return null;
        }

        public string Get(string key)
        {
            object value;

            if (_cache.TryGetValue(key, out value))
            {
                return value.ToString();
            }
            return string.Empty;
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var value = _cache.Get(key).ToString();
            return value.ToObject<T>();
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, string value)
        {
            _cache.Set(key, value);
        }

        public void Set<T>(string key, T value) where T : class
        {
            _cache.Set(key, value.ToJson());
        }

        public void Set(string key, object value, TimeSpan expiration)
        {
            _cache.Set(key, value.ToString(), expiration);
        }

        public Task SetAsync(string key, object value)
        {
            return _cache.GetOrCreateAsync(key, async factory =>
            {
                factory.SetValue(value.ToJson);
                return new
                {
                    Value = value,
                };
            });
        }

        public Task SetAsync(string key, object value, TimeSpan expiration)
        {
            return _cache.GetOrCreateAsync(key, async factory =>
            {
                factory.SetValue(value.ToJson);
                factory.AbsoluteExpiration = DateTimeOffset.Now.Add(expiration);
                return new
                {
                    Value = value,
                };
            });
        }
    }
}
