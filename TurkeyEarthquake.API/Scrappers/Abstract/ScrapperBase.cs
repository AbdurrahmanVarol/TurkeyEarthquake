using TurkeyEarthquake.API.Response;

namespace TurkeyEarthquake.API.Scrappers.Abstract
{
    public abstract class ScrapperBase
    {
        protected string BaseUrl { get; set; }
        protected string GetHtml(string url, int? pageNumbber)
        {

            if (pageNumbber is not null)
                url += $"{url}?page={pageNumbber}";

            var client = new HttpClient();

            return client.GetStringAsync(url).GetAwaiter().GetResult();
        }
        protected abstract List<EarthquakeResponse> ParseHtml(string html);

        public  List<EarthquakeResponse> GetEarthquakes(int? pageNumber)
        {
            var html = GetHtml(BaseUrl, pageNumber);

            var result = ParseHtml(html);

            return result;
        }
    }
}
