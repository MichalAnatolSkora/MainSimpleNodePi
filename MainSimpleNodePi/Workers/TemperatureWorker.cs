using System;
using System.Threading;
using System.Threading.Tasks;
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

        public TemperatureWorker(ILogger<TemperatureWorker> logger, ITemperatureReader temperatureReader)
        {
            _logger = logger;
            _temperatureReader = temperatureReader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                var temp = await _temperatureReader.ReadTemperature();
                _logger.LogInformation($"Read temperature: {temp}");
                //await _clockHub.Clients.All.ShowTime(DateTime.Now);
                await Task.Delay(1000);
            }
        }
    }
}
