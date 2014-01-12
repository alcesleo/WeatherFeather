using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFeather.Models.Services
{
    public interface IWeatherService : IDisposable
    {
        bool HasExactMatch { get; }
        void ExternalSearch(string location); // forces api call
        void Search(string location);
        void Search(double lat, double lng);
        Forecast Forecast { get; }
        IEnumerable<Forecast> ForecastAlternatives { get; }
    }
}
