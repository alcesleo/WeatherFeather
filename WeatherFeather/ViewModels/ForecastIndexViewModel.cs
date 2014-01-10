using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeatherFeather.Models;

namespace WeatherFeather.ViewModels
{
    public class ForecastIndexViewModel
    {
        public Forecast Forecast { get; set; }

        [Required(ErrorMessage = "Search field cannot be empty.")]
        public string SearchLocation { get; set; }
    }
}