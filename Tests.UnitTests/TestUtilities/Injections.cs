using Data.Repository.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.Mocks;

namespace Tests.UnitTests.TestUtilities
{
    public class Injections
    {
        public IWeatherRepository weatherRepository;
        public IDebtorsRepositoryMock debtorsRepository;

        public IWeatherService weatherService;
        public IDebtorsService debtorsService;

        public Injections()
        {
            weatherRepository = new WeatherRepositoryMock();
            debtorsRepository = new DebtorRepositoryMock();

            weatherService = new WeatherService(weatherRepository);
            debtorsService = new DebtorsService(debtorsRepository);
        }
    }
}
