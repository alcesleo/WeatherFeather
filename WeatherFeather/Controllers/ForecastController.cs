using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ActionResult Enc()
        {
            string test1 = "VÃ¤stra GÃ¶taland";
            string test2 = "Stockholm";

            byte[] bytes = Encoding.Default.GetBytes(test2);
            test1 = Encoding.UTF8.GetString(bytes);
            return View("Index");
        }


        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            var vm = new ForecastIndexViewModel();

            // null if not existans
            vm.Forecast = TempData["forecast"] as Forecast;

            return View("Index", vm);
        }

        // Forces api call
        public ActionResult ShowAlternatives(string location)
        {
            _service.ExternalSearch(location);
            if (_service.HasExactMatch)
            {
                TempData.Add("forecast", _service.Forecast);
                return RedirectToAction("Index");
            }
            else
            {
                TempData.Add("alternatives", _service.ForecastAlternatives);
                return RedirectToAction("ChooseLocation");
            }
        }

        public ActionResult LatLng(double lat, double lng)
        {
            _service.Search(lat, lng);
            TempData.Add("forecast", _service.Forecast);
            return RedirectToAction("Index");
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
