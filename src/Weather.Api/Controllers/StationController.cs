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

        [HttpGet]
        public async Task<Station> Index(int id)
        {
            return await _stationRepository.GetById(id);
        }
    }
}