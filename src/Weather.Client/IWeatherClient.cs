
using System;
using System.Threading.Tasks;

namespace Weather.Client
{
    public interface IWeatherClient
    {
        Task GetData(int stationId, DateTime startDate, DateTime endDate);
    }
}