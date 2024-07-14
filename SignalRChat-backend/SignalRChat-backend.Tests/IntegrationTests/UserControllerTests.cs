using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using SignalRChat_backend.API.Mapping.DTOs;
using System.Net;
using SignalRChat_backend.Data;

namespace SignalRChat_backend.Tests.IntegrationTests
{
    [TestFixture]
    public class UserControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;
        private IServiceScope _scope;
        private SignalRChatDbContext _dbContext;

        [SetUp]
        public void SetUp()
        {
            _scope = _factory.Services.CreateScope();
            _dbContext = _scope.ServiceProvider.GetRequiredService<SignalRChatDbContext>();
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _scope.Dispose();

            _dbContext.Dispose();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _factory.Dispose();
        }


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SignalRChatDbContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddDbContext<SignalRChatDbContext>(options =>
                        {
                            object value = options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
        }
        [Test]
        public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Act
            var response = await _client.GetAsync("/api/user");

            // Assert
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserDTO>>(stringResponse);

            Assert.AreEqual(0, users.Count);
        }

        [Test]
        public async Task CreateUser_ShouldCreateAndReturnUser()
        {
            // Arrange
            var userName = "Integration Test User";
            var content = new StringContent(JsonConvert.SerializeObject(userName), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/user", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserDTO>(stringResponse);

            Assert.NotNull(user);
            Assert.AreEqual(userName, user.Name);
        }

        [Test]
        public async Task GetUser_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userName = "Integration Test User";
            var content = new StringContent(JsonConvert.SerializeObject(userName), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/user", content);
            createResponse.EnsureSuccessStatusCode();
            var stringResponse = await createResponse.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<UserDTO>(stringResponse);

            // Act
            var response = await _client.GetAsync($"/api/user/{createdUser.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var user = JsonConvert.DeserializeObject<UserDTO>(await response.Content.ReadAsStringAsync());

            Assert.NotNull(user);
            Assert.AreEqual(createdUser.Id, user.Id);
            Assert.AreEqual(createdUser.Name, user.Name);
        }

        [Test]
        public async Task DeleteUser_ShouldDeleteUser()
        {
            // Arrange
            var userName = "Integration Test User";
            var content = new StringContent(JsonConvert.SerializeObject(userName), Encoding.UTF8, "application/json");
            var createResponse = await _client.PostAsync("/api/user", content);
            createResponse.EnsureSuccessStatusCode();
            var stringResponse = await createResponse.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<UserDTO>(stringResponse);

            // Act
            var response = await _client.DeleteAsync($"/api/user/{createdUser.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            // Verify the user was deleted
            var getResponse = await _client.GetAsync($"/api/user/{createdUser.Id}");
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}