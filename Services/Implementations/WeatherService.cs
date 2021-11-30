using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public string GetForDate(DateTime date)
        {
            return "cold";
        }

        public async Task<Weather> Insert(DateTime date, int temperatureCelsius)
        {
            var newEntity = new Weather()
            {
                Day = date,
                TemperatureCelsius = temperatureCelsius
            };
            newEntity = await _weatherRepository.Insert(newEntity);
            return newEntity;
        }

    }
}
