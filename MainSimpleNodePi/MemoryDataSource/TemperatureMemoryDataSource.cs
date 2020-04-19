using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MainSimpleNodePi.MemoryDataSource
{
    public class TemperatureMemoryDataSource : ITemperatureSource
    {
        private IMemoryCache memoryCache;

        public TemperatureMemoryDataSource(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<List<TemperatureRecord>> GetTemperature()
        {
            var dateTimes = await this.memoryCache.GetOrCreateAsync(Constants.Temperature.MemoryCacheKey, this.Factory);

            return dateTimes;
        }

        private Task<List<TemperatureRecord>> Factory(ICacheEntry entry)
        {
            return Task.FromResult(new List<TemperatureRecord>());
        }

        public async Task AddNewTemperatureRecord(TemperatureRecord temperatureRecord)
        {
            var dateTimes = await this.memoryCache.GetOrCreateAsync(Constants.Temperature.MemoryCacheKey, this.Factory);
            dateTimes.Add(temperatureRecord);
            this.memoryCache.Set(Constants.Temperature.MemoryCacheKey, dateTimes);
        }
    }
}
