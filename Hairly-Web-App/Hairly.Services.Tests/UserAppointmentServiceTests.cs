using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.UserAppointment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace Hairly.Services.Tests
{
    [TestFixture]
    public class UserAppointmentServiceTests
    {
        private ApplicationDbContext context;
        private IUserAppointmentService userAppointmentService;
        private Mock<UserManager<IdentityUser>> userManagerMock;
        private IdentityUser testUser;
        private IdentityUser testHairdresserUser;
        private Client testClient;
        private Service testService;
        private Appointment testAppointment;
        private string testHairdresserId = "hairdresser-123";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);

            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            SeedTestData();
            SetupUserManagerMock();

            userAppointmentService = new UserAppointmentService(context, userManagerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private void SeedTestData()
        {
            testHairdresserUser = new IdentityUser
            {
                Id = testHairdresserId,
                UserName = "hairdresser@hairly.com",
                Email = "hairdresser@hairly.com"
            };

            testUser = new IdentityUser
            {
                Id = "test-user-123",
                UserName = "testuser",
                Email = "test@example.com"
            };

            context.Users.Add(testHairdresserUser);
            context.Users.Add(testUser);

            testClient = new Client
            {
                Id = 1,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "0888123456",
                Email = "test@example.com",
                UserId = testUser.Id,
                HairdresserId = testHairdresserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            testService = new Service
            {
                Id = 1,
                Name = "Haircut",
                Description = "Classic haircut",
                Price = 25.00m,
                DurationInMinutes = 30,
                HairdresserId = testHairdresserId,
                IsDeleted = false
            };

            testAppointment = new Appointment
            {
                Id = 1,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                HairdresserId = testHairdresserId,
                AppointmentDate = DateTime.Now.AddDays(7),
                Status = AppointmentStatus.Scheduled,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Clients.Add(testClient);
            context.Services.Add(testService);
            context.Appointments.Add(testAppointment);
            context.SaveChanges();
        }

        private void SetupUserManagerMock()
        {
            userManagerMock
                .Setup(um => um.FindByIdAsync(testUser.Id))
                .ReturnsAsync(testUser);

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim("FirstName", "Test"),
                new System.Security.Claims.Claim("LastName", "User")
            };

            userManagerMock
                .Setup(um => um.GetClaimsAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(claims);

            var hairdressers = new List<IdentityUser> { testHairdresserUser };

            userManagerMock
                .Setup(um => um.GetUsersInRoleAsync("Hairdresser"))
                .ReturnsAsync(hairdressers);
        }

        [Test]
        public async Task GetMyAppointmentsAsync_ValidUserId_ReturnsAppointments()
        {
            var result = await userAppointmentService.GetMyAppointmentsAsync(testUser.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetMyAppointmentsAsync_InvalidUserId_ReturnsEmpty()
        {
            var result = await userAppointmentService.GetMyAppointmentsAsync("non-existent-user");

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetBookingModelAsync_ReturnsModel()
        {
            var result = await userAppointmentService.GetBookingModelAsync();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.AvailableServices);
        }

        [Test]
        public async Task BookAppointmentAsync_ValidData_ReturnsTrue()
        {
            var model = new UserAppointmentBookViewModel
            {
                ServiceId = testService.Id,
                AppointmentDate = DateTime.Now.AddDays(10)
            };

            var result = await userAppointmentService.BookAppointmentAsync(model, testUser.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task BookAppointmentAsync_InvalidServiceId_ReturnsFalse()
        {
            var model = new UserAppointmentBookViewModel
            {
                ServiceId = 999,
                AppointmentDate = DateTime.Now.AddDays(10)
            };

            var result = await userAppointmentService.BookAppointmentAsync(model, testUser.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetOrCreateClientIdForUserAsync_ExistingClient_ReturnsClientId()
        {
            var result = await userAppointmentService.GetOrCreateClientIdForUserAsync(testUser.Id);

            Assert.That(result, Is.EqualTo(testClient.Id));
        }

        [Test]
        public async Task GetOrCreateClientIdForUserAsync_NewUser_CreatesAndReturnsClientId()
        {
            var newUser = new IdentityUser
            {
                Id = "new-user-456",
                UserName = "newuser",
                Email = "newuser@example.com",
                PhoneNumber = "0888999888"
            };

            userManagerMock
                .Setup(um => um.FindByIdAsync(newUser.Id))
                .ReturnsAsync(newUser);

            var result = await userAppointmentService.GetOrCreateClientIdForUserAsync(newUser.Id);

            Assert.That(result, Is.GreaterThan(0));

            var createdClient = await context.Clients.FirstOrDefaultAsync(c => c.UserId == newUser.Id);
            Assert.IsNotNull(createdClient);
        }
    }
}