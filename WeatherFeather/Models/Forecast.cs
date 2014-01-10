using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models
{
    public partial class Forecast
    {

        // Constructor for a JSON response from SkyWatch
        public Forecast(JObject properties)
        {
            var isExact = properties.Property("city") != null;

            ForecastPeriod = new List<ForecastPeriod>();
            if (isExact)
            {
                Location = (string)properties["city"]["name"];
                Region = (string)properties["city"]["county"];
                Country = (string)properties["city"]["country"];
                Latitude = (double)properties["city"]["lat"];
                Longitude = (double)properties["city"]["lng"];

                // Yes, the key is misspelled in the response
                var forecasts = (JArray)properties["forcasts"];
                foreach (var period in forecasts)
                {
                    // TODO: Pluralize
                    ForecastPeriod.Add(new ForecastPeriod(period));
                }

                
            }
            else
            { 
                Location = (string)properties["name"];
                Region = (string)properties["county"];
                Country = (string)properties["country"];

                // These are sent as strings in this type of response
                Latitude = Convert.ToDouble((string)properties["lat"]);
                Longitude = Convert.ToDouble((string)properties["lng"]);
            }
        }

    }
}