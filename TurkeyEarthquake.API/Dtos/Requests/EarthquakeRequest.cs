using TurkeyEarthquake.API.Enums;

namespace TurkeyEarthquake.API.Dtos.Requests
{
    public class EarthquakeRequest
    {
        public WebSiteType SiteType { get; set; }
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
