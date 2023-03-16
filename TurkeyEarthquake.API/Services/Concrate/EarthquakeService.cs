using AutoMapper;
using System.Linq;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Entities;
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
        private readonly IMapper _mapper;

        public EarthquakeService(ScrapperFactoryBase scrapperFactory, ICache cache, IMapper mapper)
        {
            _scrapperFactory = scrapperFactory;
            _cache = cache;
            _mapper = mapper;
        }

        public List<EarthquakeResponse> GetEarthquakes(EarthquakeRequest request)
        {
           // var a = $"earthquakes{request.SiteType}";
            var cachedEarthquakes = _cache.Get<List<Earthquake>>($"earthquakes{request.SiteType}") ?? new List<Earthquake>();
            if(cachedEarthquakes != null && cachedEarthquakes.Any())
            {
                return _mapper.Map<List<EarthquakeResponse>>(cachedEarthquakes);
            }
            var scraper = _scrapperFactory.GetScrapper(request.SiteType);
            var earthquakes = scraper.GetEarthquakes();
            if (earthquakes.Any())
            {
                cachedEarthquakes.AddRange(_mapper.Map<List<Earthquake>>(earthquakes));

                cachedEarthquakes = cachedEarthquakes.OrderByDescending(p => p.Date).ToList();
                Console.WriteLine("---------------" + cachedEarthquakes.Count);
                _cache.Set($"earthquakes{request.SiteType}", cachedEarthquakes);
                _cache.Set($"latest{request.SiteType}", cachedEarthquakes.FirstOrDefault(),TimeSpan.FromMinutes(5));
            }



            return _mapper.Map<List<EarthquakeResponse>>(cachedEarthquakes);
        }

        public PaginatedEarthquakeResponse GetEarthquakesWithPaginated(EarthquakeRequest request)
        {
            var earthquakes = GetEarthquakes(request);
            var paginatedEarthquakes = new PaginatedEarthquakeResponse
            {
                Earthquakes = request.PageSize == null ? earthquakes : earthquakes.OrderByDescending(on => on.Date)
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
