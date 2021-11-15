using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpeedTestApi.Models;
using SpeedTestApi.Services;

namespace SpeedTestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeedTestController : ControllerBase
    {
        private readonly ILogger _logger;

        private readonly ISpeedTestEvents _eventHub;


        public SpeedTestController(ILogger<SpeedTestController> logger, ISpeedTestEvents eventHub)
        {
            _logger = logger;
            _eventHub = eventHub;

        }
        // GET speedtest/ping
        [Route("ping")]
        [HttpGet]
        public string Ping()
        {
            return "PONG";
        }

        [HttpPost]
        public async Task<string> UploadSpeedTest([FromBody] TestResult speedTest)
        {
            await _eventHub.PublishSpeedTest(speedTest);

            var response = $"Got a TestResult from { speedTest.User } with download { speedTest.Data.Speeds.Download } Mbps.";
            _logger.LogInformation(response);

            return response;
        }
    }

}