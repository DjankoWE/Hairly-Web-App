using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Client;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hairly.Services.Tests
{
    [TestFixture]
    public class ClientServiceTests
    {
        private ApplicationDbContext context;
        private IClientService clientService;
        private Client testClient;
        private string testHairdresserId = "hairdresser-123";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            SeedTestData();
            clientService = new ClientService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private void SeedTestData()
        {
            testClient = new Client
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Petrov",
                PhoneNumber = "0888123456",
                Email = "ivan@test.com",
                HairdresserId = testHairdresserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Clients.Add(testClient);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllClientsAsync_ReturnsClients()
        {
            var result = await clientService.GetAllClientsAsync(testHairdresserId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task CreateClientAsync_ValidData_ReturnsTrue()
        {
            var model = new ClientCreateViewModel
            {
                FirstName = "Maria",
                LastName = "Ivanova",
                PhoneNumber = "0888999888",
                Email = "maria@test.com"
            };

            var result = await clientService.CreateClientAsync(model, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetClientForEditAsync_ValidId_ReturnsModel()
        {
            var result = await clientService.GetClientForEditAsync(testClient.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetClientForEditAsync_InvalidId_ReturnsNull()
        {
            var result = await clientService.GetClientForEditAsync(999, testHairdresserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateClientAsync_ValidData_ReturnsTrue()
        {
            var model = new ClientEditViewModel
            {
                Id = testClient.Id,
                FirstName = "Ivan Updated",
                LastName = "Petrov",
                PhoneNumber = "0888123456",
                Email = "ivan@test.com"
            };

            var result = await clientService.UpdateClientAsync(model, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateClientAsync_InvalidId_ReturnsFalse()
        {
            var model = new ClientEditViewModel
            {
                Id = 999,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0888000000",
                Email = "test@test.com"
            };

            var result = await clientService.UpdateClientAsync(model, testHairdresserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetClientDetailsAsync_ValidId_ReturnsDetails()
        {
            var result = await clientService.GetClientDetailsAsync(testClient.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetClientForDeleteAsync_ValidId_ReturnsModel()
        {
            var result = await clientService.GetClientForDeleteAsync(testClient.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task DeleteClientAsync_ValidId_ReturnsTrue()
        {
            var result = await clientService.DeleteClientAsync(testClient.Id, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteClientAsync_InvalidId_ReturnsFalse()
        {
            var result = await clientService.DeleteClientAsync(999, testHairdresserId);

            Assert.IsFalse(result);
        }
    }
}