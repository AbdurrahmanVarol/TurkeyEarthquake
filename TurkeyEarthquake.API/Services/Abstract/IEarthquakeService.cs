using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Requests;
using TurkeyEarthquake.API.Response;

namespace TurkeyEarthquake.API.Services.Abstract
{
    public interface IEarthquakeService
    {
        List<EarthquakeResponse> GetEarthquakes(EarthquakeRequest request);
        PaginatedEarthquakeResponse GetEarthquakesWithPaginated(EarthquakeRequest request);
    }
}
