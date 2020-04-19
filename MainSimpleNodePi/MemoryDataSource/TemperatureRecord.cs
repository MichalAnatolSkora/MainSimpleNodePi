using System;

namespace MainSimpleNodePi.MemoryDataSource
{
    public struct TemperatureRecord
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}