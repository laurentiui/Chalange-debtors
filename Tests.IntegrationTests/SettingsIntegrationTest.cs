using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTests
{
    public class SettingsIntegrationTest : IClassFixture<CustomWebApplicationFactory<WebApi.Startup>>
    {
        // Uncomment to disable tests
        private class FactAttribute : Attribute { }


        private readonly CustomWebApplicationFactory<WebApi.Startup> _factory;

        public SettingsIntegrationTest(CustomWebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_SeetingsKey1MustBeTest()
        {
            string url = "/settings/getkey1";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            string text = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("key1-test", text);
        }
    }
}
