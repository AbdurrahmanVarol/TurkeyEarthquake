using Newtonsoft.Json;

namespace TurkeyEarthquake.API.Caching.Concrete.Extensions
{
    public static class StringExtension
    {
        public static T ToObject<T>(this string value) where T : class
        {
            return string.IsNullOrEmpty(value) ? null : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
