using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Factories
{
    public abstract class ScrapperFactoryBase
    {
        public abstract ScrapperBase GetScrapper(WebSiteType webSite);
    }
}
