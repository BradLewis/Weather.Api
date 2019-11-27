using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Api.Requests;
using Weather.Client;
using Weather.Client.Models;
using Weather.Data.Repositories.Interfaces;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWeatherClient _weatherClient;
        private readonly IStationRepository _stationRepository;

        public WeatherController(ILogger<WeatherController> logger, IWeatherClient weatherClient, IStationRepository stationRepository)
        {
            _logger = logger;
            _weatherClient = weatherClient;
            _stationRepository = stationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherModel>> GetWeather(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                var station = await _stationRepository.GetById(id).ConfigureAwait(false);
                return await _weatherClient.GetData(station.StationId, startDate, endDate).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occurred getting data for id", e, id);
                return new List<WeatherModel>();
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IEnumerable<WeatherModel>> GetWeatherForToday(string name)
        {
            try
            {
                var stations = await _stationRepository.GetByName(name).ConfigureAwait(false);
                var station = stations.FirstOrDefault();
                var timeNow = DateTime.Now;
                var weatherData = await _weatherClient.GetData(station.StationId, timeNow, timeNow).ConfigureAwait(false);
                return weatherData.Where(x => x.DateTime.Day == timeNow.Day);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occurred getting todays data for name", e, name);
                return new List<WeatherModel>();
            }
        }

        [HttpPost]
        public async Task<IEnumerable<WeatherModel>> PostWeather(WeatherRequest request)
        {
            try
            {
                var station = await _stationRepository.GetById(request.Id).ConfigureAwait(false);
                return await _weatherClient.GetData(station.StationId, request.StartDate, request.EndDate).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occurred getting data for request", e, request);
                return new List<WeatherModel>();
            }
        }
    }
}