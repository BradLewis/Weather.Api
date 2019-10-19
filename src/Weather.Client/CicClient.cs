using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather.Client
{
    public class CicClient : IWeatherClient
    {
        private readonly string _endpoint = "http://climate.weather.gc.ca/climate_data/bulk_data_e.html";
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CicClient(ILogger<CicClient> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        private async Task DownloadData(int stationId, DateTime startDate, DateTime endDate)
        {
            var startYear = startDate.Year;
            var endYear = endDate.Year;
            var client = _clientFactory.CreateClient();
            for (int i = startYear; i < endYear; i++)
            {
                var endpoint = $"{_endpoint}?format=csv&stationID=1706&Year={i}&Month=1&Day=14&timeframe=1&submit= Download+Data";
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var response = await client.SendAsync(request);
            }
        }

        public async Task GetData(int stationId, DateTime startDate, DateTime endDate)
        {
            await DownloadData(stationId, startDate, endDate);
        }
    }
}
