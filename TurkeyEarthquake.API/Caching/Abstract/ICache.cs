using TurkeyEarthquake.API.Caching.Concrate.Extensions;

namespace TurkeyEarthquake.API.Caching.Abstract
{
    public interface ICache
    {
        void Set(string key, string value);

        void Set<T>(string key, T value) where T : class;

        Task SetAsync(string key, object value);

        void Set(string key, object value, TimeSpan expiration);

        Task SetAsync(string key, object value, TimeSpan expiration);

        T Get<T>(string key) where T : class;

        string Get(string key);

        Task<T> GetAsync<T>(string key) where T : class;

        void Remove(string key);
    }
}
