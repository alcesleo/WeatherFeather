using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherFeather.Controllers
{
    public class ForecastController : Controller
    {
        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            var data = new Webservices.YrNoWebservice().GetWeatherData();
            return View();
        }

    }
}
