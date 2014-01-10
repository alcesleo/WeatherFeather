using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherFeather.Models.Repositories
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {

        /*
         * Forecast
         */

        protected abstract IQueryable<Forecast> QueryForecasts();

        public IEnumerable<Forecast> GetForecasts()
        {
            return QueryForecasts().ToList();
        }

        public Forecast GetForecastById(int forecastId)
        {
            return QueryForecasts().SingleOrDefault(f => f.ForecastID == forecastId);
        }

        public Forecast GetForecastByLatLng(double lat, double lng)
        {
            return QueryForecasts().SingleOrDefault(f => f.Latitude == lat && f.Longitude == lng);
        }

        public Forecast GetForecastByLocation(string location)
        {
            return QueryForecasts().SingleOrDefault(f => f.Location == location);
        }

        public abstract void InsertForecast(Forecast forecast);
        public abstract void UpdateForecast(Forecast forecast);
        public abstract void DeleteForecast(int forecastId);

        /*
         * ForecastPeriod
         */

        protected abstract IQueryable<ForecastPeriod> QueryForecastPeriods();

        public IEnumerable<ForecastPeriod> GetForecastPeriods()
        {
            return QueryForecastPeriods().ToList();
        }

        public ForecastPeriod GetForecastPeriodById(int forecastPeriodId)
        {
            return QueryForecastPeriods().SingleOrDefault(f => f.ForecastPeriodID == forecastPeriodId);
        }

        public abstract void InsertForecastPeriod(ForecastPeriod forecastPeriod);
        public abstract void UpdateForecastPeriod(ForecastPeriod forecastPeriod);
        public abstract void DeleteForecastPeriod(int forecastPeriodId);

        /*
         * Other
         */
        public abstract void Save();

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

    }
}