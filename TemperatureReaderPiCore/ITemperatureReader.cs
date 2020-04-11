using System.Threading.Tasks;

namespace TemperatureReaderPiCore
{
    public interface ITemperatureReader
    {
        Task<decimal> ReadTemperature();
    }
}