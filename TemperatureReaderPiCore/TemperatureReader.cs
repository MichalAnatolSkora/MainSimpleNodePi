using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iot.Device.OneWire;

namespace TemperatureReaderPiCore
{
    public class TemperatureReader : ITemperatureReader
    {
        public async Task<decimal> ReadTemperature()
        {
            // you can remove condition from "FirstOrDefault"
            var devices = OneWireThermometerDevice.EnumerateDevices().ToList();

            Console.WriteLine("Devices START");
            devices.ForEach(e => Console.WriteLine($"DeviceId: {e.DeviceId}"));
            Console.WriteLine("END Devices");

            var oneWireThermometerDevice = devices.FirstOrDefault();
            if (oneWireThermometerDevice == null)
            {
                Console.WriteLine("Cannot find any device");
                return await Task.FromResult(-100m);
            }

            var temp = await oneWireThermometerDevice.ReadTemperatureAsync();
            return (decimal) temp.Celsius;
        }
    }
}
