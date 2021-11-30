using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain.Dto.Weather
{
    public class WeatherAddDto
    {
        public DateTime Day { get; set; }
        public int TemperatureCelsius { get; set; }
    }
}
