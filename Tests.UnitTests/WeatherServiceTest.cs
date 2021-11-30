using Data.Repository;
using Data.Repository.Implementations;
using Moq;
using NUnit.Framework;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.UnitTests.Mocks;
using Tests.UnitTests.TestUtilities;

namespace Tests.UnitTests
{
    public class WeatherServiceTest
    {
        private IWeatherService _weatherService;

        [SetUp]
        public void Setup()
        {
            var injections = new Injections();
            _weatherService = injections.weatherService;
        }

        [Test]
        public void Test_GetForDateReturn_Cold()
        {
            Assert.AreEqual("cold", _weatherService.GetForDate(DateTime.Now));
        }

        [Test]
        public async Task Test_Insert_Returns10DegreesCold()
        {
            var result = await _weatherService.Insert(DateTime.Now, 10);

            Assert.AreEqual(10, result.TemperatureCelsius);
            Assert.AreEqual("cold", result.Summary);
        }
    }
}
