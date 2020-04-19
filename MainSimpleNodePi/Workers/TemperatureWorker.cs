using System;
using System.Threading;
using System.Threading.Tasks;
using MainSimpleNodePi.Hubs;
using MainSimpleNodePi.MemoryDataSource;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TemperatureReaderPiCore;

namespace MainSimpleNodePi.Workers
{
    public class TemperatureWorker : BackgroundService
    {
        private readonly ILogger<TemperatureWorker> logger;
        private readonly ITemperatureReader temperatureReader;
        private readonly IHubContext<TemperatureHub> temperatureHubContext;
        private readonly ITemperatureSource temperatureSource;

        public TemperatureWorker(
            ILogger<TemperatureWorker> logger,
            ITemperatureReader temperatureReader,
            IHubContext<TemperatureHub> temperatureHubContext,
            ITemperatureSource temperatureSource)
        {
            this.logger = logger;
            this.temperatureReader = temperatureReader;
            this.temperatureHubContext = temperatureHubContext;
            this.temperatureSource = temperatureSource;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                this.logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                var temp = await this.temperatureReader.ReadTemperature();
                this.logger.LogInformation($"Read temperature: {temp}");
                await this.temperatureHubContext.Clients.All.SendAsync("ReceiveTemperature", temp, cancellationToken: stoppingToken);
                var temperatureRecord = new TemperatureRecord {Date = DateTime.Now, Value = temp};
                await this.temperatureSource.AddNewTemperatureRecord(temperatureRecord);
            }
        }
    }
}
