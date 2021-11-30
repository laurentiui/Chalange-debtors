using Data.Domain.Entity;
using Data.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.UnitTests.Mocks
{
    internal class WeatherRepositoryMock : BaseRepositoryMock<Weather>, IWeatherRepository
    {
    }
}
