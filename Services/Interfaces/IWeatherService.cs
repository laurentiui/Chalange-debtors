using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IWeatherService
    {
        string GetForDate(DateTime date);
        Task<Weather> Insert(DateTime date, int temperatureCelsius);
    }
}

