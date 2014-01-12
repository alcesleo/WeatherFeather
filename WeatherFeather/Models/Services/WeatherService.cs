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
        public IEnumerable<Forecast> ForecastAlternatives
        {
            get
            {
                // Facade
                return Webservice.ForecastAlternatives;
            }
        }


        #region housekeeping

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

        #endregion housekeeping


        // Search through API no matter if cache exists
        public void ExternalSearch(string location)
        {
            Webservice.Search(location);
            if (Webservice.HasExactMatch)
            {
                Forecast = Webservice.Forecast;
                UpdateCache();
            }
        }


        public void Search(string location)
        {
            // Get from db
            Forecast forecast = null;
            try
            {
                forecast = _repository.GetForecastByLocation(location);
            }
            catch (InvalidOperationException)
            {
                // This means that there's probably more than one hit
            }

            if (IsCurrent(forecast))
            {
                Forecast = forecast;
            }
            else
            {
                Webservice.Search(location);
                if (Webservice.HasExactMatch)
                {
                    Forecast = Webservice.Forecast;
                    UpdateCache(forecast);
                }
            }

        }

        public void Search(double lat, double lng)
        {
            // Get from db
            var forecast = _repository.GetForecastByLatLng(lat, lng);
            if (IsCurrent(forecast))
            {
                Forecast = forecast;
            }
            else
            {
                Webservice.Search(lat, lng); // Will never have alternatives
                Forecast = Webservice.Forecast;
                UpdateCache(forecast);
            }
        }

        /// <summary>
        /// Replaces or inserts forecast with current Webservice state
        /// </summary>
        /// <param name="forecast">Optional forecast to update</param>
        private void UpdateCache(Forecast forecast = null)
        {
            if (!Webservice.HasExactMatch)
            {
                throw new Exception("This method can only be called when Webservice has a match.");
            }

            // Delete cache
            if (forecast != null)
            {
                _repository.DeleteForecast(forecast.ForecastID);
            }

            // Cache
            _repository.InsertForecast(Webservice.Forecast);
            _repository.Save();
        }

        private bool IsCurrent(Forecast forecast)
        {
            // Returns true only if forecast exists and is younger than 2 hours

            if (forecast == null)
            {
                return false;
            }
            else
            { 
                var age = forecast.LastUpdated - DateTime.Now;
                return age.Hours < 2;
            }
        }




        public void Dispose()
        {
            // TODO: correct?
            _repository.Dispose();
        }
    }
}