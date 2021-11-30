using Data.Domain.Dto.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTests
{
    public class UserIntegrationTest
        : IClassFixture<CustomWebApplicationFactory<WebApi.Startup>>
    {
        private readonly CustomWebApplicationFactory<WebApi.Startup> _factory;

        public UserIntegrationTest(CustomWebApplicationFactory<WebApi.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test_NonRegisterdUserCanNotLogin()
        {
            string url = "/account/login";
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var nonExistingUser = new UserLoginDto()
            {
                Email = $"{Guid.NewGuid()}@test.com",
                Password = "does-not-matter"
            };
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(nonExistingUser), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Test_RegisterWorkflowIsGood()
        {
            //=============== Register user
            // Arrange
            var client = _factory.CreateClient();

            // Act
            string url = "/account/register";
            var newUser = TestUtilities.Users.NewUser("register-workflow");

            var userRegisterDto = new UserRegisterDto()
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password
            };
            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(userRegisterDto), Encoding.UTF8)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            });

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var text = await response.Content.ReadAsStringAsync();
            var registeredButNotConfirmedUser = JsonConvert.DeserializeObject<RegisteredUserDto>(text);

            Assert.NotNull(registeredButNotConfirmedUser);
            Assert.NotNull(registeredButNotConfirmedUser.ConfirmToken);

            //=============== Confirm user
            // Act
            url = $"/account/confirm/{registeredButNotConfirmedUser.ConfirmToken}";

            response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            text = await response.Content.ReadAsStringAsync();
            var authentificatedUser = JsonConvert.DeserializeObject<AuthentificatedUserDto>(text);

            Assert.NotNull(authentificatedUser);
            Assert.NotNull(authentificatedUser.Token);

            //Assert.Equal("token", authentificatedUser.Token);


        }
    }
}
