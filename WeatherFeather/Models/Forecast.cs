using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public class Forecast
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Location { get; set; }

        public List<Day> Days { get; set; }
    }
}