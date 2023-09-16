using Microsoft.AspNetCore.Mvc;
using TurkeyEarthquake.API.Dtos.Requests;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Services.Abstract;

namespace TurkeyEarthquake.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarthquakesController : ControllerBase
    {
        private readonly IEarthquakeService _earthquakeService;

        public EarthquakesController(IEarthquakeService earthquakeService)
        {
            _earthquakeService = earthquakeService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] WebSiteType webSiteType)
        {
            var result = _earthquakeService.GetEarthquakes(webSiteType);
            return Ok(result);
        }
        [HttpGet("paginated")]
        public IActionResult GetEarthquakesWithPaginated([FromQuery] EarthquakeRequest earthquakeRequest)
        {
            var result = _earthquakeService.GetEarthquakesWithPaginated(earthquakeRequest);
            return Ok(result);
        }

    }
}
