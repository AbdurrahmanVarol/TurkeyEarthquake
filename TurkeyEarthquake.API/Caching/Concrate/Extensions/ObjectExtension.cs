using Newtonsoft.Json;

namespace TurkeyEarthquake.API.Caching.Concrate.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
