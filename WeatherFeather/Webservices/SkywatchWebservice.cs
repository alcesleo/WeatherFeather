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
    public class SkywatchWebservice
    {
        public Forecast GetForecast(string location)
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

            var obj = JObject.Parse(rawJson);

            // Throw if returned json is an error-message
            if (obj.Property("Error") != null)
            {
                throw new Exception((string)obj.Property("Cause").Value);
            }

            return new Forecast();

            //if (JArray.Parse(rawJson).Contains(
            //return JArray.Parse(rawJson).Select(t => new Day(t)).ToList();
        }
    }
}