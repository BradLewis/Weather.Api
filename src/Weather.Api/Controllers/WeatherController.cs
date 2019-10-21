using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Client;
using Weather.Client.Models;
using System.Threading.Tasks;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IWeatherClient _client;

        public WeatherController(ILogger<WeatherController> logger, IWeatherClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherModel>> Get()
        {
            var startDate = new DateTime(2000, 1, 1);
            var endDate = new DateTime(2002, 1, 1);
            var stationId = 1706;
            return await _client.GetData(stationId, startDate, endDate);
        }
    }
}