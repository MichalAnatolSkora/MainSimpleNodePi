using System.Threading.Tasks;

namespace TemperatureReaderPiCore
{
    public class FakeTemperatureReader : ITemperatureReader
    {
        public async Task<decimal> ReadTemperature()
        {
            return await Task.FromResult(40m);
        }
    }
}
