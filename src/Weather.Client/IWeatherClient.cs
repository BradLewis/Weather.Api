using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Client.Models;

namespace Weather.Client
{
    public interface IWeatherClient
    {
        Task<IEnumerable<WeatherModel>> GetData(int stationId, DateTime startDate, DateTime endDate);
    }
}