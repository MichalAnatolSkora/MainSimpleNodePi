using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TemperatureReaderPiCore;

namespace MainSimpleNodePi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureReader _temperatureReader;

        public TemperatureController(ITemperatureReader temperatureReader)
        {
            _temperatureReader = temperatureReader;
        }

        [HttpGet]
        public async Task<decimal> Get()
        {
            return await _temperatureReader.ReadTemperature();
        }
    }
}
