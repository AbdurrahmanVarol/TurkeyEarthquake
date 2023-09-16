using AutoMapper;
using TurkeyEarthquake.API.Dtos.Response;
using TurkeyEarthquake.API.Entities;

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
