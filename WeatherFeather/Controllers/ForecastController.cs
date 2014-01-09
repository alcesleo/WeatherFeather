using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherFeather.Webservices;

namespace WeatherFeather.Controllers
{
    public class ForecastController : Controller
    {
        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            var days = new List<Models.ForecastPeriod>();
            
            //days.Add(new Models.Day {
            //    AirPressure = 2.3,
            //    CreatedAt = DateTime.Today,
            //    WindDirection = "SW",
            //    Percipitation = 0.0,
            //    Symbol = 4,
            //    Temperature = 4.6,
            //    WindSpeed = 200
            //});  
            //days.Add(new Models.Day {
            //    AirPressure = 2.3,
            //    CreatedAt = DateTime.Today,
            //    WindDirection = "SW",
            //    Percipitation = 0.0,
            //    Symbol = 4,
            //    Temperature = 4.6,
            //    WindSpeed = 200
            //});  
            //var forecasts = new Models.Forecast {
            //    Region = "Göteborg",
            //    Country = "SE",
            //    Location = "Lunden",
            //    Days = days
            //};
            var forecast = new SkywatchWebservice().GetForecast("Göteborg");
            return View("Forecast", forecast);
        }

    }
}
