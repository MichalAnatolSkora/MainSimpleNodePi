using System;
using System.Threading;
using System.Threading.Tasks;
using MainSimpleNodePi.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TemperatureReaderPiCore;

namespace MainSimpleNodePi.Workers
{
    public class TemperatureWorker : BackgroundService
    {
        private readonly ILogger<TemperatureWorker> _logger;
        private readonly ITemperatureReader _temperatureReader;
        private readonly IHubContext<TemperatureHub> _temperatureHubContext;

        public TemperatureWorker(ILogger<TemperatureWorker> logger, ITemperatureReader temperatureReader, IHubContext<TemperatureHub> temperatureHubContext)
        {
            _logger = logger;
            _temperatureReader = temperatureReader;
            _temperatureHubContext = temperatureHubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                var temp = await _temperatureReader.ReadTemperature();
                _logger.LogInformation($"Read temperature: {temp}");
                await this._temperatureHubContext.Clients.All.SendAsync("ReceiveTemperature", temp, cancellationToken: stoppingToken);
            }
        }
    }
}
