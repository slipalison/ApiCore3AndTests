using ApiCore3AndTests.Client.V1;
using ApiCore3AndTests.Domain.Shared;
using ApiCore3AndTests.Test.StartServer;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ApiCore3AndTests.Test
{
    public class UnitTest1 : StartTestServer
    {
        private IWeatherForecastClient _weatherForecastClient;

        public UnitTest1()
        {
            var httpClientFactory = Substitute.For<IHttpClientFactory>();
            httpClientFactory
                    .CreateClient(nameof(WeatherForecastClient))
                    .Returns(client);

            _weatherForecastClient = new WeatherForecastClient(httpClientFactory);
        }

        [Fact]
        public async Task Test1()
        {
            var response = await _weatherForecastClient.CreateWeatherForecast(new WeatherForecastIncludeCommand(), "teste");
            var result = await _weatherForecastClient.GetAllWeatherForecast("teste");
            Assert.Contains(result.Value, x => x.Date >= DateTime.Now.AddDays(1));
        }
    }
}