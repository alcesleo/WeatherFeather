using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public partial class ForecastPeriod
    {

        //public ForecastPeriod(JToken dayToken)
        //{
        //    Temperature = (double)dayToken["temperature"];
        //    AirPressure = (double)dayToken["pressure"];
        //    WindDirection = (string)dayToken["winddirection"];
        //    WindSpeed = (double)dayToken["windspeed"];
        //    Symbol = (int)dayToken["symbol"];
        //    Percipitation = (double)dayToken["percipitation"];

        //    Date = DateTime.ParseExact((string)dayToken["date"],
        //        "ddd MMM dd HH:mm:ss zz00 yyyy", CultureInfo.InvariantCulture);
        //}
    }
}