using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainSimpleNodePi.Sensors
{
    public class TemperatureReader : ITemperatureReader
    {
        public decimal ReadTemperature()
        {
            return 15;
        }
    }
}
