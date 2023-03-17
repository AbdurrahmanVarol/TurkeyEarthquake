using HtmlAgilityPack;
using TurkeyEarthquake.API.Response;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Scrappers.Concrete.HtmlAgilityPack
{
    public class HapKandilliScrapper : ScrapperBase
    {
        public HapKandilliScrapper()
        {
            BaseUrl = "http://www.koeri.boun.edu.tr/scripts/lst9.asp";
        }
        protected override List<EarthquakeResponse> ParseHtml(string html)
        {
            var data = new List<EarthquakeResponse>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var htmlData = htmlDoc
                           .DocumentNode
                           .SelectNodes("//pre").FirstOrDefault().InnerHtml;

            var rows = htmlData.Split(Environment.NewLine).Skip(7).Select(p => p.Trim()).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            foreach (var row in rows)
            {
                var rowData = row.Split(" ").Select(p => p.Trim()).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
                var locationList = rowData.GetRange(8, rowData.Count - 1 - 8);
                var location = string.Join("", locationList);
                var earthquake = new EarthquakeResponse
                {
                    Date = DateTime.Parse(rowData[0]).Add(TimeSpan.Parse(rowData[1])),
                    Latitude = Math.Round(double.Parse(rowData[2].Replace(".", ",")), 3),
                    Longitude = Math.Round(double.Parse(rowData[3].Replace(".", ",")), 3),
                    Depth = double.Parse(rowData[4].Replace(".", ",")),
                    Magnitude = double.Parse(rowData[6].Replace(".", ",")),
                    Type = "ML",
                    Location = location
                };
                data.Add(earthquake);
            }
            return data;
        }
    }
}
