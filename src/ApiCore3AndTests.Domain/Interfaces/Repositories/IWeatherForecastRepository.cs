using ApiCore3AndTests.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCore3AndTests.Domain.Interfaces.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<IAsyncEnumerable<WeatherForecast>> GetAll();

        Task Add(WeatherForecast weatherForecast);
    }
}