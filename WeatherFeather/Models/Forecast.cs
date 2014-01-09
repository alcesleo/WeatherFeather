using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public partial class Forecast
    {
        public Forecast()
        {
            // Empty
        }

        public Forecast(JObject properties)
        {
            
        }
    }
}