using AutoMapper;
using Data.Domain.Dto.Weather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService, IMapper mapper)
        {
            _logger = logger;
            _weatherService = weatherService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get specified items for weather forecast
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     here we add extra documention we want to see in swagger
        ///
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = _weatherService.GetForDate(DateTime.UtcNow)
            })
            .ToArray();
        }

        /// <summary>
        /// Get specified items for weather forecast
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     here we add extra documention we want to see in swagger
        ///
        /// </remarks>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpPost]
        public async Task<WeatherDto> Add([FromBody] WeatherAddDto weatherAddDto)
        {
            //_logger.LogInformation("here i want to log - just because");
            //_logger.LogCritical("something CRTITICAL HAPPEND");
            var createdWeather = await _weatherService.Insert(weatherAddDto.Day, weatherAddDto.TemperatureCelsius);
            var returnEntity = _mapper.Map<WeatherDto>(createdWeather);
            return returnEntity;
        }
    }
}
