using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Client;
using Weather.Client.Models;
using System.Threading.Tasks;
using Weather.Data.Repositories.Interfaces;
using Weather.Data.Models;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWeatherClient _client;
        private readonly IStationRepository _stationRepository;

        public WeatherController(ILogger<WeatherController> logger, IWeatherClient client, IStationRepository stationRepository)
        {
            _logger = logger;
            _client = client;
            _stationRepository = stationRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherModel>> Get()
        {
            _logger.LogInformation("called!");
            var startDate = new DateTime(2000, 1, 1);
            var endDate = new DateTime(2002, 1, 1);
            var stationId = 1706;
            return await _client.GetData(stationId, startDate, endDate);
        }
    }
}