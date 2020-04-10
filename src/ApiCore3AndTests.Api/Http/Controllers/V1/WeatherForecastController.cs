using ApiCore3AndTests.Domain.Entities;
using ApiCore3AndTests.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ApiCore3AndTests.Api.Http.Controllers.V1
{
    [ApiController, ApiVersion("1.0")]
    [Route("api/V{version:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get([Required, FromHeader(Name = "X-Correlation-Id")] string correlationId)
        {
            _logger.LogInformation("Log do bem correlation {0}", correlationId);

            var t = await _weatherForecastService.GetAll();
            return Ok(t);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<IActionResult> Post([FromBody]WeatherForecast weatherForecast, [Required, FromHeader(Name = "X-Correlation-Id")] string correlationId)
        {
            _logger.LogInformation("Log do bem correlation {0}", correlationId);

            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var rng = new Random();
            var t = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(rng.Next(1, 30)),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            };

            await _weatherForecastService.Insert(t);
            return Ok();
        }
    }
}