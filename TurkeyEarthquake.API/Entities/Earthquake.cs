﻿namespace TurkeyEarthquake.API.Entities
{
    public class Earthquake
    {
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Depth { get; set; }
        public string Type { get; set; }
        public double Magnitude { get; set; }
        public string Location { get; set; }
    }
}
