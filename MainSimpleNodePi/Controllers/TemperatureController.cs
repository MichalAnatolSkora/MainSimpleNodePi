using System.Collections.Generic;
using MainSimpleNodePi.Sensors;
using Microsoft.AspNetCore.Mvc;

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
        public decimal Get()
        {
            return _temperatureReader.ReadTemperature();
        }
    }
}
