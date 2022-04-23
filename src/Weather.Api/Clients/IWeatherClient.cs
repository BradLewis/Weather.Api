using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Api.Models;

namespace Weather.Api.Clients
{
    public interface IWeatherClient
    {
        Task<IEnumerable<WeatherModel>> GetData(int stationId, DateOnly startDate, DateOnly endDate);
    }
}