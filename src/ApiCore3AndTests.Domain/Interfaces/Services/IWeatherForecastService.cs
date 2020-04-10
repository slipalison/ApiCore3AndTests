using ApiCore3AndTests.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Domain.Interfaces.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetAll();

        Task Insert(WeatherForecast weatherForecast);
    }
}