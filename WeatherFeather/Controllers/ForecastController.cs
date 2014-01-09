using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherFeather.Models;
using WeatherFeather.Webservices;
using WeatherFeather.ViewModels;

namespace WeatherFeather.Controllers
{
    public class ForecastController : Controller
    {
        private IWeatherService _service;

        public ForecastController()
            :this(new WeatherService())
        {
            // Empty
        }

        public ForecastController(IWeatherService service)
        {
            _service = service;
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            return View("Index", new ForecastIndexViewModel());
        }

        //
        // POST: /Forecast/

        public ActionResult Index(ForecastIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //whatever.Search(viewModel.SearchLocation);
                //if (whatever.HasLocationAlternatives)
                // TempData.Add("Locations", whatever.Locations);
                // redirect to locations
            }
            return View();
        }

    }
}
