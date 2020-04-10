using System;

namespace ApiCore3AndTests.Domain.Entities
{
    public class WeatherForecast
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}