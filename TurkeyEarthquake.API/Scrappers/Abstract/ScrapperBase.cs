using TurkeyEarthquake.API.Response;

namespace TurkeyEarthquake.API.Scrappers.Abstract
{
    public abstract class ScrapperBase
    {
        protected string BaseUrl { get; set; }
        protected Task<string> GetHtml(string url, int? pageNumbber)
        {

            if (pageNumbber is not null)
                url += $"{url}?page={pageNumbber}";

            var client = new HttpClient();

            return client.GetStringAsync(url);
        }
        protected abstract Task<List<EarthquakeResponse>> ParseHtml(string html);

        public  async Task<List<EarthquakeResponse>> GetEarthquakes(int? pageNumber)
        {
            var html = await GetHtml(BaseUrl,pageNumber);

            var result = await  ParseHtml(html);

            return result;
        }
    }
}
