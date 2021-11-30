using Data.Domain.Entity;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
    public class WeatherRepository : BaseRepository<Weather>, IWeatherRepository
    {
        private readonly AppDbContext _appDbContext;

        public WeatherRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
