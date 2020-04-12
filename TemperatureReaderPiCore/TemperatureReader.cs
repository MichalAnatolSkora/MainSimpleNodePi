using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.OneWire;

namespace TemperatureReaderPiCore
{
    public class TemperatureReader : ITemperatureReader
    {
        private readonly OneWireThermometerDevice oneWireThermometerDevice;
        public TemperatureReader()
        {
            var devices = OneWireThermometerDevice.EnumerateDevices().ToList();
            Console.WriteLine("Devices START");
            devices.ForEach(e => Console.WriteLine($"DeviceId: {e.DeviceId}"));
            Console.WriteLine("END Devices");
            this.oneWireThermometerDevice = devices.FirstOrDefault();
            if (oneWireThermometerDevice == null)
            {
                Console.WriteLine("Cannot find any device");
            }
        }

        public async Task<decimal> ReadTemperature()
        {
            if (oneWireThermometerDevice == null)
            {
                Console.WriteLine("Cannot find any device -> -100");
                return await Task.FromResult(-100m);
            }

            Stopwatch s = Stopwatch.StartNew();
            var temp = await oneWireThermometerDevice.ReadTemperatureAsync();
            var temperatureInCelsius = (decimal) temp.Celsius;
            s.Stop();
            Console.WriteLine($"Reading of temperature took {s.ElapsedMilliseconds} milliseconds");
            return temperatureInCelsius;
        }
    }
}
