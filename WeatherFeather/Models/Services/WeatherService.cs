using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherFeather.Models.Repositories;
using WeatherFeather.Models.Services;
using WeatherFeather.Webservices;

namespace WeatherFeather.Models.Services
{
    public class WeatherService : IWeatherService
    {
        public bool HasExactMatch
        {
            get
            {
                return Forecast != null;
            }
        }
        public Forecast Forecast { get; private set; }
        public IEnumerable<Forecast> ForecastAlternatives { get; private set; }

        private IWeatherRepository _repository;
        private SkywatchWebservice _webservice;
        private SkywatchWebservice Webservice
        {
            get
            {
                // Lazy init
                if (_webservice == null)
                {
                    _webservice = new SkywatchWebservice();
                }
                return _webservice;
            }
        }

        public WeatherService()
            :this(new EFWeatherRepository())
        {
            // Empty
        }

        public WeatherService(IWeatherRepository repository)
        {
            _repository = repository;
        }

        public void Search(string location)
        {
            var forecast = _repository.GetForecastByLocation(location);
            if (forecast != null)
            {
                Forecast = forecast;
            }
            else
            {
                Webservice.Search(location);
                if (Webservice.HasExactMatch)
                {
                    Forecast = Webservice.Forecast;
                }
                else
                {
                    ForecastAlternatives = Webservice.ForecastAlternatives;
                }
            }
        }

        public void Search(double lat, double lng)
        {
            var forecast = _repository.GetForecastByLatLng(lat, lng);
            if (forecast != null)
            {
                Forecast = forecast;
            }
            else
            {
                Webservice.Search(lat, lng); // Will never have choices
                Forecast = Webservice.Forecast;
            }
      
        }

        public void Dispose()
        {
            // TODO: correct?
            _repository.Dispose();
        }
    }
}