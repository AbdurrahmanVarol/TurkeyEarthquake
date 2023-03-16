using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Scrappers.Abstract;
using TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack;

namespace TurkeyEarthquake.API.Factories
{
    public class ScrapperFactory : ScrapperFactoryBase
    {
        public override ScrapperBase GetScrapper(WebSiteType webSite)
        {
            return webSite switch
            {
                WebSiteType.afad => new HapAfadScraper(),
                WebSiteType.kandilli => new HapKandilliScrapper(),
                _ => throw new ArgumentException()
            };
        }
    }
}
