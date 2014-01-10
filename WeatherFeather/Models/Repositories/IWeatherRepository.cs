using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherFeather.Models.Repositories
{
    public interface IWeatherRepository : IDisposable
    {
        IEnumerable<Forecast> GetForecasts();
        Forecast GetForecastById(int forecastId);
        Forecast GetForecastByLatLng(double lat, double lng);
        Forecast GetForecastByLocation(string location);
        void InsertForecast(Forecast forecast);
        void UpdateForecast(Forecast forecast);
        void DeleteForecast(int forecastId);

        IEnumerable<ForecastPeriod> GetForecastPeriods();
        ForecastPeriod GetForecastPeriodById(int forecastPeriodId);
        void InsertForecastPeriod(ForecastPeriod forecastPeriod);
        void UpdateForecastPeriod(ForecastPeriod forecastPeriod);
        void DeleteForecastPeriod(int forecastPeriodId);

        void Save();
    }
}
