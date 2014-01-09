using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherFeather.Models;

namespace WeatherFeather.ViewModels
{
    public class ForecastIndexViewModel
    {
        public Forecast Forecast { get; set; }

        public string SearchLocation { get; set; }
    }
}