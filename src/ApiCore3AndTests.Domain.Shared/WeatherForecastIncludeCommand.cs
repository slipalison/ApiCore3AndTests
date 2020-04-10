using System;

namespace ApiCore3AndTests.Domain.Shared
{
    public class WeatherForecastIncludeCommand
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }
}