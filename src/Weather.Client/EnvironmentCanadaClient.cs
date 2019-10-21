using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Collections.Generic;
using System.Linq;
using Weather.Client.Models;
using Weather.Client.Maps;

namespace Weather.Client
{
    public class EnvironmentCanadaClient : IWeatherClient
    {
        private readonly string _endpoint = "http://climate.weather.gc.ca/climate_data/bulk_data_e.html";
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _clientFactory;

        public EnvironmentCanadaClient(ILogger<EnvironmentCanadaClient> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        private async Task<IEnumerable<WeatherModel>> ReadCsv(HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var csvReader = new CsvReader(reader))
            {
                csvReader.Configuration.RegisterClassMap<EnvironmentCanadaMap>();
                csvReader.Configuration.MissingFieldFound = null;
                var records = csvReader.GetRecords<WeatherModel>().ToList();
                return records;
            }
        }

        private async Task<IEnumerable<WeatherModel>> DownloadData(int stationId, DateTime startDate, DateTime endDate)
        {
            var startYear = startDate.Year;
            var endYear = endDate.Year;
            var client = _clientFactory.CreateClient();

            var endpoint = $"{_endpoint}?format=csv&stationID={stationId}&Year={startYear}&Month=1&Day=14&timeframe=1&submit=Download+Data";
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await client.SendAsync(request);
            var result = await ReadCsv(response);
            return result;
        }

        public async Task<IEnumerable<WeatherModel>> GetData(int stationId, DateTime startDate, DateTime endDate)
        {
            return await DownloadData(stationId, startDate, endDate);
        }
    }
}
