using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using Weather.Client.Models;
using System.Collections.Generic;

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

        private async Task<IEnumerable<EnvrionmentCanadaEntry>> ReadCsv(string csvString)
        {
            
            var reader = new StreamReader(filename);
            using (var csvReader = new CsvReader(reader))
            {
                var records = csvReader.GetRecords<EnvrionmentCanadaEntry>();
                return records;
            }
        }

        private async Task DownloadData(int stationId, DateTime startDate, DateTime endDate)
        {
            var startYear = startDate.Year;
            var endYear = endDate.Year;
            var client = _clientFactory.CreateClient();
            for (int i = startYear; i < endYear; i++)
            {
                var endpoint = $"{_endpoint}?format=csv&stationID={stationId}&Year={i}&Month=1&Day=14&timeframe=1&submit=Download+Data";
                var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                ReadCsv(responseString);
                var x = 0;
            }
        }

        public async Task GetData(int stationId, DateTime startDate, DateTime endDate)
        {
            await DownloadData(stationId, startDate, endDate);
        }
    }
}
