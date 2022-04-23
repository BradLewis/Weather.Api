using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Api.Maps;
using Weather.Api.Models;
using Weather.Api.Readers;

namespace Weather.Api.Clients
{
    public class EnvironmentCanadaClient : IWeatherClient
    {
        private readonly EndpointSettings _endpointSettings;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICsvReader _csvReader;
        private readonly ILogger<EnvironmentCanadaClient> _logger;

        public EnvironmentCanadaClient(IHttpClientFactory clientFactory, ICsvReader csvReader, IOptions<EndpointSettings> endpointSettings, ILogger<EnvironmentCanadaClient> logger)
        {
            _clientFactory = clientFactory;
            _endpointSettings = endpointSettings.Value;
            _csvReader = csvReader;
            _logger = logger;
        }

        private async Task<IEnumerable<WeatherModel>> DownloadData(int stationId, DateOnly startDate, DateOnly endDate)
        {
            startDate = new DateOnly(startDate.Year, startDate.Month, 1);
            endDate = new DateOnly(endDate.Year, endDate.Month, 1);
            List<WeatherModel> weatherModels = new List<WeatherModel>();

            _logger.LogInformation("Fetching data from Environment Canada, with {stationId}, {startDate}, {endDate}", stationId, startDate, endDate);
            using (var client = _clientFactory.CreateClient())
            {
                var date = startDate;
                while (date <= endDate)
                {
                    var endpoint = $"{_endpointSettings.EnvironmentCanada}?format=csv&stationID={stationId}&Year={date.Year}&Month={date.Month}&Day=14&timeframe=1&submit=Download+Data";
                    var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                    var response = await client.SendAsync(request);
                    var result = await _csvReader.ReadCsv<EnvironmentCanadaMap>(response);
                    weatherModels.AddRange(result);

                    date = date.AddMonths(1);
                }
            }
            return weatherModels;
        }

        public async Task<IEnumerable<WeatherModel>> GetData(int stationId, DateOnly startDate, DateOnly endDate)
        {
            return await DownloadData(stationId, startDate, endDate);
        }
    }
}