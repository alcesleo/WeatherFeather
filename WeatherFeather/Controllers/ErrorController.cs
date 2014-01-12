using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherFeather.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error()
        {
            // For some exceptionally stupid reason, this only works if it is located in
            // the Shared views, and not in the Error views.
            return View("Error");
        }

        public ActionResult Error404()
        {
            return View("Error404");
        }

    }
}
