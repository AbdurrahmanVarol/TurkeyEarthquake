using System.Linq;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Requests;
using TurkeyEarthquake.API.Response;
using TurkeyEarthquake.API.Scrappers.Abstract;
using TurkeyEarthquake.API.Services.Abstract;

namespace TurkeyEarthquake.API.Services.Concrate
{
    public class EarthquakeService : IEarthquakeService
    {

        private readonly ScrapperFactoryBase _scrapperFactory;
        private readonly ICache _cache;

        public EarthquakeService(ScrapperFactoryBase scrapperFactory, ICache cache)
        {
            _scrapperFactory = scrapperFactory;
            _cache = cache;
        }

        public List<EarthquakeResponse> GetEarthquakes(EarthquakeRequest request)
        {
            var scraper = _scrapperFactory.GetScrapper(request.SiteType);
            var earthquakes = scraper.GetEarthquakes();
            return earthquakes;
        }

        public PaginatedEarthquakeResponse GetEarthquakesWithPaginated(EarthquakeRequest request)
        {
            var scraper = _scrapperFactory.GetScrapper(request.SiteType);
            var earthquakes = scraper.GetEarthquakes();
            var paginatedEarthquakes = new PaginatedEarthquakeResponse
            {
                Earthquakes = request.PageSize == null ? earthquakes : earthquakes.OrderBy(on => on.Date)
                                                      .Skip((int)((request.PageNumber - 1) * request.PageSize))
                                                      .Take((int)request.PageSize)
                                                      .ToList(),
                CurrentPage = request.PageNumber,
                TotalPage = (int)Math.Round(earthquakes.Count / (double)request.PageSize)
            };
            return paginatedEarthquakes;
        }
    }
}
