using System.Collections.Generic;
using System.Threading.Tasks;

namespace MainSimpleNodePi.MemoryDataSource
{
    public interface ITemperatureSource
    {
        Task<List<TemperatureRecord>> GetTemperature();
        Task AddNewTemperatureRecord(TemperatureRecord temperatureRecord);
    }
}