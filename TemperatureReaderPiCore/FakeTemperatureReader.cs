using System.Threading.Tasks;

namespace TemperatureReaderPiCore
{
    public class FakeTemperatureReader : ITemperatureReader
    {
        private readonly decimal[] _temperatures = { 15, 16, 17, 18, 19, 20, 21, 22 };
        private int _indexToUse;

        public async Task<decimal> ReadTemperature()
        {
            if (_indexToUse >= _temperatures.Length)
            {
                _indexToUse = 0;
            }

            return await Task.FromResult(_temperatures[_indexToUse++]);
        }
    }
}
