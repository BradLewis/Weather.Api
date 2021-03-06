﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Client.Maps;
using Weather.Client.Models;

namespace Weather.Client
{
    public class EnvironmentCanadaClient : IWeatherClient
    {
        private readonly string _endpoint;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ICsvReader _csvReader;

        public EnvironmentCanadaClient(IHttpClientFactory clientFactory, ICsvReader csvReader, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _endpoint = configuration.GetValue<string>("Endpoints:EnvironmentCanada");
            _csvReader = csvReader;
        }

        private async Task<IEnumerable<WeatherModel>> DownloadData(int stationId, DateTime startDate, DateTime endDate)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, 1);
            endDate = new DateTime(endDate.Year, endDate.Month, 1);
            List<WeatherModel> weatherModels = new List<WeatherModel>();

            using (var client = _clientFactory.CreateClient())
            {
                var date = startDate;
                while (date <= endDate)
                {
                    var endpoint = $"{_endpoint}?format=csv&stationID={stationId}&Year={date.Year}&Month={date.Month}&Day=14&timeframe=1&submit=Download+Data";
                    var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                    var response = await client.SendAsync(request).ConfigureAwait(false);
                    var result = await _csvReader.ReadCsv<EnvironmentCanadaMap>(response).ConfigureAwait(false);
                    weatherModels.AddRange(result);

                    date = date.AddMonths(1);
                }
            }
            return weatherModels;
        }

        public async Task<IEnumerable<WeatherModel>> GetData(int stationId, DateTime startDate, DateTime endDate)
        {
            return await DownloadData(stationId, startDate, endDate).ConfigureAwait(false);
        }
    }
}