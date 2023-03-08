using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TurkeyEarthquake.API.Enums;
using TurkeyEarthquake.API.Factories;
using TurkeyEarthquake.API.Requests;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarthquakesController : ControllerBase
    {
        private readonly ScrapperFactoryBase _scrapperFactory;

        public EarthquakesController(ScrapperFactoryBase scrapperFactory)
        {
            _scrapperFactory = scrapperFactory;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] EarthquakeRequest earthquakeRequest) {
            
            var scrapper = _scrapperFactory.GetScrapper(WebSiteType.afad);

            var result = scrapper.GetEarthquakes(earthquakeRequest.PageNumber);

            return Ok(result);        
        
        }
        
    }
}
