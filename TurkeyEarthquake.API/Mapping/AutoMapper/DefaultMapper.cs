using AutoMapper;
using TurkeyEarthquake.API.Entities;
using TurkeyEarthquake.API.Response;

namespace TurkeyEarthquake.API.Mapping.AutoMapper
{
    public class DefaultMapper : Profile
    {
        public DefaultMapper()
        {
            CreateMap<Earthquake,EarthquakeResponse>().ReverseMap();
        }
    }
}
