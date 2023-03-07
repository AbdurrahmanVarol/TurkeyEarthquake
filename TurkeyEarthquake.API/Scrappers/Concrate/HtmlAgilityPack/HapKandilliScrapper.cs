using TurkeyEarthquake.API.Response;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack
{
    public class HapKandilliScrapper : ScrapperBase
    {
        public HapKandilliScrapper()
        {
            BaseUrl = "http://www.koeri.boun.edu.tr/scripts/lst9.asp";
        }
        protected override Task<List<EarthquakeResponse>> ParseHtml(string html)
        {
            throw new NotImplementedException();
        }
    }
}
