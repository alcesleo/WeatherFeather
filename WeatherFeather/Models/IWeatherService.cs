using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFeather.Models
{
    interface IWeatherService : IDisposable
    {
        bool HasExactMatch;
        void Search(string location);
        void Search(double lat, double lng);
        Forecast Forecast;
        IEnumerable<Forecast> ForecastAlternatives;
    }
}
