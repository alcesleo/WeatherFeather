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
        public ForecastPeriod()
        {
            // Empty
        }

        public ForecastPeriod(JToken properties)
        {
            AirPressure = Convert.ToDouble((string)properties["pressure"], CultureInfo.InvariantCulture);
            Temperature = Convert.ToDouble((string)properties["temp"], CultureInfo.InvariantCulture);
            WindSpeed = Convert.ToDouble((string)properties["windspeed"], CultureInfo.InvariantCulture);
            WindDirection = (string)properties["winddirection"];
            Symbol = Convert.ToInt32((string)properties["symbol"]);
            Date = DateTime.Parse((string)properties["date"]);

            // Percipitation can be empty (always the last two)
            // FIXME: This code is really horrendously ugly
            double percipitation = 0.0;
            Double.TryParse((string)properties["percipitation"], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out percipitation);
            Percipitation = percipitation;
        }

    }
}