﻿using TurkeyEarthquake.API.Dtos.Response;

namespace TurkeyEarthquake.API.Scrappers.Abstract
{
    public abstract class ScrapperBase
    {
        protected string BaseUrl { get; set; }
        protected string GetHtml(string url)
        {
            var client = new HttpClient();

            return client.GetStringAsync(url).GetAwaiter().GetResult();
        }
        protected abstract List<EarthquakeResponse> ParseHtml(string html);

        public List<EarthquakeResponse> GetEarthquakes()
        {
            var html = GetHtml(BaseUrl);

            var result = ParseHtml(html);

            return result;
        }
    }
}
