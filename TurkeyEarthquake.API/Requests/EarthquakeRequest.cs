﻿using TurkeyEarthquake.API.Enums;

namespace TurkeyEarthquake.API.Requests
{
    public class EarthquakeRequest
    {
        public WebSiteType SiteType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int? PageSize { get; set;}
    }
}
