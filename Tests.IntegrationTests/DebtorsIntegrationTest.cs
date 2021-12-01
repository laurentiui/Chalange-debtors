using Data.Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi;
using Xunit;

namespace Tests.IntegrationTests
{
    public class DebtorsIntegrationTest : IClassFixture<CustomWebApplicationFactory<WebApi.Startup>> {
        // Uncomment to disable tests
        //private class FactAttribute : Attribute { }

        private readonly CustomWebApplicationFactory<WebApi.Startup> _factory;

        public DebtorsIntegrationTest(CustomWebApplicationFactory<WebApi.Startup> factory) {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Endpoint_List() {
            string url = "/debtors/list";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());


            var text = await response.Content.ReadAsStringAsync();
            var debtors = JsonConvert.DeserializeObject<Debtor[]>(text);

            Assert.True(debtors.Count() > 0);
        }

        [Fact]
        public async Task Insert_WorksAndIGetTheEntityBack() {
            //string url = "/weatherforecast/";
            //// Arrange
            //var client = _factory.CreateClient();

            //// Act
            //var newWeather = new WeatherAddDto() {
            //    Day = DateTime.Now,
            //    TemperatureCelsius = 25
            //};
            //var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(newWeather), Encoding.UTF8) {
            //    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            //});

            //// Assert
            //response.EnsureSuccessStatusCode(); // Status Code 200-299
            //Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

        }
    }
}