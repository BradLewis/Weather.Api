using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Data.Models;
using Weather.Data.Repositories.Interfaces;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IStationRepository _stationRepository;
        private readonly ILogger _logger;
        
        public StationController(IStationRepository stationRepository, ILogger<StationController> logger)
        {
            _stationRepository = stationRepository;
            _logger = logger;
        }

        [HttpGet("id/{id}")]
        public async Task<Station> GetById(int id)
        {
            return await _stationRepository.GetById(id);
        }

        [HttpGet("name/{name}")]
        public async Task<IEnumerable<Station>> GetByName(string name)
        {
            return await _stationRepository.GetByName(name);
        }
    }
}