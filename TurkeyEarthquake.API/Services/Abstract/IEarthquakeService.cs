using TurkeyEarthquake.API.Dtos.Requests;
using TurkeyEarthquake.API.Dtos.Response;
using TurkeyEarthquake.API.Enums;

namespace TurkeyEarthquake.API.Services.Abstract
{
    public interface IEarthquakeService
    {
        List<EarthquakeResponse> GetEarthquakes(WebSiteType webSiteType);
        PaginatedResponse GetEarthquakesWithPaginated(EarthquakeRequest request);
    }
}
