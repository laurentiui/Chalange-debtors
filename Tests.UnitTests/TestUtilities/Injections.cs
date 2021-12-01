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
        public IUserRepository userRepository;
        public IWeatherRepository weatherRepository;
        public IDebtorsRepositoryMock debtorsRepository;

        public IUserService userService;
        public IWeatherService weatherService;
        public IDebtorsService debtorsService;

        public Injections()
        {
            weatherRepository = new WeatherRepositoryMock();
            userRepository = new UserRepositoryMock();
            debtorsRepository = new DebtorRepositoryMock();

            userService = new UserService(userRepository);
            weatherService = new WeatherService(weatherRepository);
            debtorsService = new DebtorsService(debtorsRepository);
        }
    }
}
