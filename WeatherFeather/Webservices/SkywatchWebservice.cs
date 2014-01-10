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
        public List<Forecast> ForecastAlternatives { get; set; }
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
            var url = String.Format("http://skywatch.code-monkey.se/adapter.php?action=2&lng={0}&lat={1}", lat, lng);
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
            var responseObject = GetParsedJsonFromUrl(url);
            RaiseForError(responseObject);
            return ParseResponse(responseObject);
        }

        /// <summary>
        /// Parses a response object into either a Forecast or ForecastAlternatives.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if forecast, false if alternatives</returns>
        private bool ParseResponse(JObject obj)
        {
            if (IsExactMatch(obj))
            {
                Forecast = new Forecast(obj);
                return true;
            }
            else
            {
                foreach (var f in obj)
                {
                    ForecastAlternatives.Add(new Forecast(obj));
                }
                return false;
            }
        }

        /// <summary>
        /// Gets a JObject from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private JObject GetParsedJsonFromUrl(string url)
        {
            var rawJson = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            // Parse
            return JObject.Parse(rawJson);
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