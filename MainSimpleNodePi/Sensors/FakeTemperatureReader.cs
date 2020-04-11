using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainSimpleNodePi.Sensors
{
    public class FakeTemperatureReader : ITemperatureReader
    {
        public decimal ReadTemperature()
        {
            return 40;
        }
    }
}
