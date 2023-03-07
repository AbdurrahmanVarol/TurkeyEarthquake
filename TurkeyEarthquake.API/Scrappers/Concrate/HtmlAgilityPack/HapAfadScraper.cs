using System.Runtime.CompilerServices;
using TurkeyEarthquake.API.Response;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack
{
    public class HapAfadScraper : ScrapperBase
    {
        public HapAfadScraper()
        {
            BaseUrl = "https://deprem.afad.gov.tr/last-earthquakes.html";
        }
        protected override Task<List<EarthquakeResponse>> ParseHtml(string html)
        {
            throw new NotImplementedException();
        }
    }
}
