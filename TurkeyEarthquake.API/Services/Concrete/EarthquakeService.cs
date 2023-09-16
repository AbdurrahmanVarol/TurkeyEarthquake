using AutoMapper;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Dtos.Requests;
using TurkeyEarthquake.API.Dtos.Response;
using TurkeyEarthquake.API.Entities;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Services.Abstract;

namespace TurkeyEarthquake.API.Services.Concrete
{
    public class EarthquakeService : IEarthquakeService
    {


        private readonly ScrapperFactoryBase _scrapperFactory;
        private readonly ICache _cache;
        private readonly IMapper _mapper;

        public EarthquakeService(ScrapperFactoryBase scrapperFactory, ICache cache, IMapper mapper)
        {
            _scrapperFactory = scrapperFactory;
            _cache = cache;
            _mapper = mapper;
        }

        public List<EarthquakeResponse> GetEarthquakes(WebSiteType webSiteType)
        {
            // var a = $"earthquakes{request.SiteType}";
            var cachedEarthquakes = _cache.Get<List<Earthquake>>($"earthquakes{webSiteType}") ?? new List<Earthquake>();
            if (cachedEarthquakes.Any())
                return _mapper.Map<List<EarthquakeResponse>>(cachedEarthquakes);

            var scraper = _scrapperFactory.GetScrapper(webSiteType);
            var earthquakes = scraper.GetEarthquakes();
            if (earthquakes.Any())
            {
                cachedEarthquakes.AddRange(_mapper.Map<List<Earthquake>>(earthquakes));
                cachedEarthquakes = cachedEarthquakes.OrderByDescending(p => p.Date).ToList();
                _cache.Set($"earthquakes{webSiteType}", cachedEarthquakes, TimeSpan.FromMinutes(5));
                _cache.Set($"latest{webSiteType}", cachedEarthquakes.FirstOrDefault(), TimeSpan.FromMinutes(5));
            }
            return _mapper.Map<List<EarthquakeResponse>>(cachedEarthquakes);
        }

        public PaginatedResponse GetEarthquakesWithPaginated(EarthquakeRequest request)
        {
            var earthquakes = GetEarthquakes(request.SiteType);
            var paginatedEarthquakes = new PaginatedResponse
            {
                Items = earthquakes.OrderByDescending(on => on.Date).Skip(request.Index * request.Size).Take(request.Size).ToList(),
                Count = earthquakes.Count,
                Index = request.Index,
                Size = request.Size,
            };
            return paginatedEarthquakes;
        }
    }
}
