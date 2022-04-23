using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<IEnumerable<WeatherModel>> DownloadData(int stationId, DateOnly date)
        {
            List<WeatherModel> weatherModels = new List<WeatherModel>();

            _logger.LogInformation("Fetching data from Environment Canada, with {stationId}, {date}", stationId, date);
            using var client = _clientFactory.CreateClient();
            var endpoint = $"{_endpointSettings.EnvironmentCanada}?format=csv&stationID={stationId}&Year={date.Year}&Month={date.Month}&Day=14&time=LST&timeframe=1&submit=Download+Data";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await client.SendAsync(request);
            var result = await _csvReader.ReadCsv<EnvironmentCanadaMap>(response);
            return result;
        }
        private IEnumerable<DateOnly> GetDates(DateOnly startDate, DateOnly endDate)
        {
            var dates = new List<DateOnly>();
            for (var date = startDate; date <= endDate; date = date.AddMonths(1))
            {
                dates.Add(date);
            }
            return dates;
        }

        public async Task<IEnumerable<WeatherModel>> GetData(int stationId, DateOnly startDate, DateOnly endDate)
        {
            startDate = new DateOnly(startDate.Year, startDate.Month, 1);
            endDate = new DateOnly(endDate.Year, endDate.Month, 1);
            var dates = GetDates(startDate, endDate);

            var tasks = dates.Select(date => DownloadData(stationId, date));
            var results = await Task.WhenAll(tasks);

            return results.SelectMany(result => result);
        }
    }
}