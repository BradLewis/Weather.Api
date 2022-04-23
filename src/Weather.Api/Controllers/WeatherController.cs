using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Api.Models;
using Weather.Api.Clients;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWeatherClient _weatherClient;
        private readonly IStationsClient _stationsClient;

        public WeatherController(ILogger<WeatherController> logger, IWeatherClient weatherClient, IStationsClient stationsClient)
        {
            _logger = logger;
            _weatherClient = weatherClient;
            _stationsClient = stationsClient;
        }

        [HttpGet("id/{id}")]
        public async Task<IEnumerable<WeatherModel>> GetWeather(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("Received request for weather. {id}, {startDate}, {endDate}", id, startDate, endDate);
                var station = await _stationsClient.GetById(id);
                var startDateOnly = DateOnly.FromDateTime(startDate);
                var endDateOnly = DateOnly.FromDateTime(endDate);
                if (startDateOnly == DateOnly.MinValue)
                    startDateOnly = new DateOnly(station.FirstYear, 1, 1);
                if (endDateOnly == DateOnly.MinValue)
                    endDateOnly = new DateOnly(station.LastYear, 1, 1);
                return await _weatherClient.GetData(station.StationId, startDateOnly, endDateOnly);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occurred getting data for id. {exception}, {id}", e, id);
                return new List<WeatherModel>();
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IEnumerable<WeatherModel>> GetWeatherForToday(string name)
        {
            try
            {
                _logger.LogInformation("Received request for weather for today. {name}", name);
                var stations = await _stationsClient.GetByName(name);
                var station = stations.FirstOrDefault();
                var timeNow = DateOnly.FromDateTime(DateTime.Now);
                var weatherData = await _weatherClient.GetData(station.StationId, timeNow, timeNow);
                return weatherData.Where(x => x.DateTime.Day == timeNow.Day);
            }
            catch (Exception e)
            {
                _logger.LogError("Error occurred getting todays data for name. {exception}, {name}", e, name);
                return new List<WeatherModel>();
            }
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok("Running");
        }
    }
}