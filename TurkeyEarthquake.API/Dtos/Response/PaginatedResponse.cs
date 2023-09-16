namespace TurkeyEarthquake.API.Dtos.Response;

public class PaginatedResponse
{
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public IList<EarthquakeResponse> Items { get; set; } = new List<EarthquakeResponse>();
    public int Pages => (int)Math.Ceiling(Count / (double)Size);
    public bool HasPrevious => Index > 0;
    public bool HasNext => Index + 1 < Count;
}
