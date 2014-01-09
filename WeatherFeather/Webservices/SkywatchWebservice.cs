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
        private JObject _responseObject = null;

        private JObject ResponseObject 
        {
            get
            { 
                if (_responseObject == null) 
                {
                    throw new Exception("No search has been made");
                }
                return _responseObject;
            }
        }

        public bool HasLocationAlternatives
        {
            get
            {
                return ResponseObject.Root.Type == JTokenType.Array;
            }
        }

        public Forecast Forecast
        {
            get
            {
                return new Forecast(ResponseObject);
            }
        }

        public void Search(string location)
        {
            var rawJson = string.Empty;

            var requestUriString = String.Format("http://skywatch.code-monkey.se/adapter.php?action=1&city={0}", location); 
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);
            request.Method = "GET";
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            _responseObject = JObject.Parse(rawJson);

            // Throw if returned json is an error-message
            if (_responseObject.Property("Error") != null)
            {
                throw new Exception((string)_responseObject.Property("Cause").Value);
            }
        }
    }
}