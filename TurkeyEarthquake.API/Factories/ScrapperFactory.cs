using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Scrappers.Abstract;
using TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack;

namespace TurkeyEarthquake.API.Factories
{
    public class ScrapperFactory : ScrapperFactoryBase
    {
        public override ScrapperBase GetScrapper(WebSiteType webSite)
        {
            switch (webSite)
            {
                case WebSiteType.afad:
                    return new HapAfadScraper();
                case WebSiteType.kandilli:
                    return new HapKandilliScrapper();
                default:
                    throw new ArgumentException()
            }
        }
    }
}
