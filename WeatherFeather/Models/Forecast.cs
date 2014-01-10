using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public partial class Forecast
    {

        // Constructor for a JSON response from SkyWatch
        public Forecast(JObject properties)
        {
            // Forecast
            Location = (string)properties["city"]["name"];
            Region = (string)properties["city"]["county"];
            Country = (string)properties["city"]["country"];
            Latitude = (double)properties["city"]["lat"];
            Longitude = (double)properties["city"]["lng"];

            // Periods
            ForecastPeriod = new List<ForecastPeriod>();
            var forecasts = (JArray)properties["forcasts"]; // Yes, the key is misspelled in the response
            foreach (var period in forecasts)
            {
                // TODO: Pluralize
                ForecastPeriod.Add(new ForecastPeriod(period));
            }
        }

        // Iterating over a JArray gives JTokens
        public Forecast(JToken properties)
        {
            Location = (string)properties["name"];
            Region = (string)properties["county"];
            Country = (string)properties["country"];

            // These are sent as strings in this type of response
            Latitude = Convert.ToDouble((string)properties["lat"], CultureInfo.InvariantCulture);
            Longitude = Convert.ToDouble((string)properties["lng"], CultureInfo.InvariantCulture);
        }

    }
}