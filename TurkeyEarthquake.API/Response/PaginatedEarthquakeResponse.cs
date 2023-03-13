namespace TurkeyEarthquake.API.Response
{
    public class PaginatedEarthquakeResponse
    {
        public List<EarthquakeResponse> Earthquakes { get;set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
