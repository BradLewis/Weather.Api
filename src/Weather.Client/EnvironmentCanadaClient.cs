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
        private readonly IHttpClientFactory _clientFactory;

        public EnvironmentCanadaClient(IHttpClientFactory clientFactory)
        {
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
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            endDate = new DateTime(endDate.Year, endDate.Month, 1);
            List<WeatherModel> weatherModels = new List<WeatherModel>();

            using (var client = _clientFactory.CreateClient())
            {
                var date = startDate;
                while (date < endDate)
                {

                    var endpoint = $"{_endpoint}?format=csv&stationID={stationId}&Year={date.Year}&Month={date.Month}&Day=14&timeframe=1&submit=Download+Data";
                    var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                    var response = await client.SendAsync(request);
                    var result = await ReadCsv(response);
                    weatherModels.AddRange(result);

                    date = date.AddMonths(1);
                }
            }
            return weatherModels;
        }

        public async Task<IEnumerable<WeatherModel>> GetData(int stationId, DateTime startDate, DateTime endDate)
        {
            return await DownloadData(stationId, startDate, endDate);
        }
    }
}
