using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Requests;
using TurkeyEarthquake.API.Scrappers.Abstract;
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
        public IActionResult Get([FromQuery] EarthquakeRequest earthquakeRequest)
        {
            var result = _earthquakeService.GetEarthquakes(earthquakeRequest);
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
