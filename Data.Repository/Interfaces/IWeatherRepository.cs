using Data.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IWeatherRepository : IBaseRepository<Weather>
    {
    }
}
