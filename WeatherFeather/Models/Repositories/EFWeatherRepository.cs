using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WeatherFeather.Models.DataModels;

namespace WeatherFeather.Models.Repositories
{
    public class EFWeatherRepository : WeatherRepositoryBase
    {
        // Renaming this was a bitch, here's how:
        // http://stackoverflow.com/questions/2060278/the-entitycontainer-name-must-be-unique-an-entitycontainer-with-the-name-entit
        private WeatherEntities _context = new WeatherEntities();

        // Forecasts

        protected override IQueryable<Forecast> QueryForecasts()
        {
            return _context.Forecast.AsQueryable();
        }


        public override void InsertForecast(Forecast forecast)
        {
            _context.Forecast.Add(forecast);
        }

        public override void UpdateForecast(Forecast forecast)
        {
            _context.Entry(forecast).State = EntityState.Modified;
        }

        public override void DeleteForecast(int forecastId)
        {
            Forecast forecast = _context.Forecast.Find(forecastId);
            _context.Forecast.Remove(forecast);
        }

        // ForecastPeriods

        protected override IQueryable<ForecastPeriod> QueryForecastPeriods()
        {
            return _context.ForecastPeriod.AsQueryable();
        }

        public override void InsertForecastPeriod(ForecastPeriod forecastPeriod)
        {
            _context.ForecastPeriod.Add(forecastPeriod);
        }

        public override void UpdateForecastPeriod(ForecastPeriod forecastPeriod)
        {
            _context.Entry(forecastPeriod).State = EntityState.Modified;
        }

        public override void DeleteForecastPeriod(int forecastPeriodId)
        {
            ForecastPeriod forecastPeriod = _context.ForecastPeriod.Find(forecastPeriodId);
            _context.ForecastPeriod.Remove(forecastPeriod);
        }

        // Other

        public override void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
            base.Dispose(disposing);
        }
    }
}