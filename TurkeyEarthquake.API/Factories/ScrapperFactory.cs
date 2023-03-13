using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Scrappers.Abstract;
using TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack;

namespace TurkeyEarthquake.API.Factories
{
    public class ScrapperFactory : ScrapperFactoryBase
    {
        private readonly ICache _cache;
        public ScrapperFactory(ICache cache)
        {
            _cache = cache;
        }
        public override ScrapperBase GetScrapper(WebSiteType webSite)
        {
            return webSite switch
            {
                WebSiteType.afad => new HapAfadScraper(_cache),
                WebSiteType.kandilli => new HapKandilliScrapper(_cache),
                _ => throw new ArgumentException()
            };
        }
    }
}
