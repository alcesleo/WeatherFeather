using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public class Day
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double AirPressure { get; set; }
        public string WindDirection { get; set; }
        public double WindSpeed { get; set; }
        // TODO: enum
        public int Symbol { get; set; }
        public double Percipitation { get; set; }
    }
}