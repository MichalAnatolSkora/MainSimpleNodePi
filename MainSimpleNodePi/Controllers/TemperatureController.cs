using System.Collections.Generic;
using MainSimpleNodePi.MemoryDataSource;

namespace MainSimpleNodePi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TemperatureReaderPiCore;

    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly ITemperatureSource temperatureSource;

        public TemperatureController(ITemperatureSource temperatureSource)
        {
            this.temperatureSource = temperatureSource;
        }

        [HttpGet]
        public async Task<List<TemperatureRecord>> Get()
        {
            return await this.temperatureSource.GetTemperature();
        }
    }
}
