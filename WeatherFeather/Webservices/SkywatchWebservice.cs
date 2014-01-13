using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using WeatherFeather.Models;
using System.Globalization;

namespace WeatherFeather.Webservices
{
    /**
     * Connects to Emil Carlssons wrapper API for yr.no and geonames, since it's JSON and a lot simpler.
     * 
     * Multiple locations:
     * http://skywatch.code-monkey.se/adapter.php?action=1&city=stockholm 
     * 
     * One location:
     * 
     * http://skywatch.code-monkey.se/adapter.php?action=1&city=kalmar 
     * 
     * Coordinates:
     * http://skywatch.code-monkey.se/adapter.php?action=2&lng=17.9&lat=59.6
     * 
     * Error:
     * http://skywatch.code-monkey.se/adapter.php?action=1&city=asdf
     */

    public class SkywatchWebservice
    {

        #region Public

        public Forecast Forecast { get; set; }

        private List<Forecast> _alternatives = new List<Forecast>();
        public IEnumerable<Forecast> ForecastAlternatives 
        {
            get
            {
                // Protect privacy leak
                return _alternatives.AsReadOnly();
            }
        }

        public bool HasExactMatch
        {
            get
            {
                return Forecast != null;
            }
        }


        public bool Search(string location)
        {
            var url = String.Format("http://skywatch.code-monkey.se/adapter.php?action=1&city={0}", location);
            return MakeRequest(url);
        }

        public bool Search(double lat, double lng)
        {
            var url = String.Format("http://skywatch.code-monkey.se/adapter.php?action=2&lat={0}&lng={1}", 
                lat.ToString(CultureInfo.InvariantCulture), // Force dot
                lng.ToString(CultureInfo.InvariantCulture));
            return MakeRequest(url);
        }

        #endregion

        private bool IsExactMatch(JObject response)
        {
            return response.Root.Type != JTokenType.Array;
        }

        /// <summary>
        /// Make a request to the provided url, parses and throws errors and everything!
        /// </summary>
        /// <param name="url"></param>
        /// <returns>True if a forecast was found, false if multiple locations were matched</returns>
        private bool MakeRequest(string url)
        { 
            var response = GetJsonFromUrl(url);

            // if array
            if (response.StartsWith("["))
            {
                ParseAlternatives(response);
                return false;
            }
            // if object
            else if (response.StartsWith("{"))
            {
                ParseForecast(response);
                return true;
            }
            throw new Exception("Invalid response json");
        }

        private void ParseAlternatives(string jsonArray)
        {
            var arr = JArray.Parse(jsonArray);
            foreach (var f in arr)
            {
                _alternatives.Add(new Forecast(f));
            }
        }

        private void ParseForecast(string jsonObject)
        {
            var obj = JObject.Parse(jsonObject);
            RaiseForError(obj); // an object can also be an error message
            Forecast = new Forecast(obj);
            Forecast.LastUpdated = DateTime.Now;
        }

        private string GetJsonFromUrl(string url)
        {
            var rawJson = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            return rawJson;
        }

        /// <summary>
        /// If the response object is an error-message, raises an error. Otherwise does nothing.
        /// </summary>
        /// <param name="response"></param>
        private void RaiseForError(JObject response)
        {
            if (response.Property("Error") != null)
            {
                throw new Exception((string)response.Property("Cause").Value);
            }
        }

    }
}