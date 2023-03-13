using HtmlAgilityPack;
using System.Runtime.CompilerServices;
using TurkeyEarthquake.API.Caching.Abstract;
using TurkeyEarthquake.API.Response;
using TurkeyEarthquake.API.Scrappers.Abstract;

namespace TurkeyEarthquake.API.Scrappers.Concrate.HtmlAgilityPack
{
    public class HapAfadScraper : ScrapperBase
    {
        public HapAfadScraper(ICache cache)
        {
            Cache = cache;
            BaseUrl = "https://deprem.afad.gov.tr/last-earthquakes.html";
        }
        protected override List<EarthquakeResponse> ParseHtml(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var rows =
                htmlDoc
                    .DocumentNode
                    .SelectNodes("//table/tbody/tr");
            List<EarthquakeResponse> data = new();

            foreach (var row in rows)
            {
                double a = 7.8;
                var x = row.SelectNodes("//td");
                try
                {
                    var rowData = row.InnerText.Split(Environment.NewLine).Where(p => !string.IsNullOrWhiteSpace(p.Trim())).Select(p => p.Trim()).ToList();
                    var earthquake = new EarthquakeResponse
                    {
                        Date = DateTime.Parse(row.ChildNodes[0].InnerHtml),
                        Latitude = double.Parse(row.ChildNodes[1].InnerHtml.Replace(".", ",")),
                        Longitude = double.Parse(row.ChildNodes[2].InnerHtml.Replace(".", ",")),
                        Depth = double.Parse(row.ChildNodes[3].InnerHtml.Replace(".", ",")),
                        Type = row.ChildNodes[4].InnerHtml,
                        Magnitude = double.Parse(row.ChildNodes[5].InnerHtml.Replace(".", ",")),
                        Location = row.ChildNodes[6].InnerHtml
                    };

                    data.Add(earthquake);
                }
                catch (Exception exception)
                {
                    return null;
                }

            }
            return data.OrderByDescending(p => p.Date).ToList();
        }
    }
}
