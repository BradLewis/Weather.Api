using System;
using Microsoft.Extensions.Logging;

namespace Weather.Client
{
    public class CicClient : IWeatherClient
    {
        private readonly string _endpoint = "http://climate.weather.gc.ca/climate_data/bulk_data_e.html";
        private readonly ILogger _logger;

        public CicClient(ILogger<CicClient> logger)
        {
            _logger = logger;
        }

        public async Task GetData(int stationId, DateTime startDate, DateTime endDate)
        {

        }
    }
}
