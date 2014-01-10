using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherFeather.Models;
using WeatherFeather.Models.Services;
using WeatherFeather.Webservices;
using WeatherFeather.ViewModels;

namespace WeatherFeather.Controllers
{
    public class ForecastController : Controller
    {

        #region housekeeping

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

        #endregion

        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            return View("Index", new ForecastIndexViewModel());
        }

        public ActionResult LatLng(double lat, double lng)
        {
            _service.Search(lat, lng);
            // TODO: Errors
            return View("Index", new ForecastIndexViewModel { Forecast = _service.Forecast });
        }

        //
        // POST: /Forecast/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ForecastIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _service.Search(viewModel.SearchLocation);
                if (_service.HasExactMatch)
                {
                    var vm = new ForecastIndexViewModel();
                    vm.Forecast = _service.Forecast;
                    return View("Index", vm);
                }
                else
                {
                    TempData.Add("alternatives", _service.ForecastAlternatives);
                    return RedirectToAction("ChooseLocation");
                }
            }
            // TODO: errors
            return View("Index", viewModel);
        }

        public ActionResult ChooseLocation()
        {
            var alternatives = TempData["alternatives"] as IEnumerable<Forecast>;
            // TODO: Errors
            return View("ChooseLocation", alternatives);
        }

    }
}
