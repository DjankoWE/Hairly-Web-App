using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Appointment;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hairly.Services.Tests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private ApplicationDbContext context;
        private IAppointmentService appointmentService;
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
            SeedTestData();
            appointmentService = new AppointmentService(context);
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
                ClientId = 1,
                ServiceId = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                HairdresserId = testHairdresserId,
                Status = AppointmentStatus.Scheduled,
                CreatedOn = DateTime.Now,
                IsDeleted = false
            };

            context.Clients.Add(testClient);
            context.Services.Add(testService);
            context.Appointments.Add(testAppointment);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllAppointmentsAsync_ReturnsNonDeletedAppointments()
        {
            var result = await appointmentService.GetAllAppointmentsAsync(testHairdresserId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetAppointmentCreateModelAsync_ReturnsModel()
        {
            var result = await appointmentService.GetAppointmentCreateModelAsync(testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task CreateAppointmentAsync_ValidData_ReturnsTrue()
        {
            var model = new AppointmentCreateViewModel
            {
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                AppointmentDate = DateTime.Now.AddDays(3)
            };

            var result = await appointmentService.CreateAppointmentAsync(model, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAppointmentForEditAsync_ValidId_ReturnsModel()
        {
            var result = await appointmentService.GetAppointmentForEditAsync(testAppointment.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAppointmentForEditAsync_InvalidId_ReturnsNull()
        {
            var result = await appointmentService.GetAppointmentForEditAsync(999, testHairdresserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateAppointmentAsync_ValidData_ReturnsTrue()
        {
            var model = new AppointmentEditViewModel
            {
                Id = testAppointment.Id,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                AppointmentDate = DateTime.Now.AddDays(5),
                Status = AppointmentStatus.Completed
            };

            var result = await appointmentService.UpdateAppointmentAsync(model, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task UpdateAppointmentAsync_InvalidId_ReturnsFalse()
        {
            var model = new AppointmentEditViewModel
            {
                Id = 999,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                AppointmentDate = DateTime.Now.AddDays(5),
                Status = AppointmentStatus.Completed
            };

            var result = await appointmentService.UpdateAppointmentAsync(model, testHairdresserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetAppointmentDetailsAsync_ValidId_ReturnsDetails()
        {
            var result = await appointmentService.GetAppointmentDetailsAsync(testAppointment.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAppointmentDetailsAsync_InvalidId_ReturnsNull()
        {
            var result = await appointmentService.GetAppointmentDetailsAsync(999, testHairdresserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAppointmentForDeleteAsync_ValidId_ReturnsModel()
        {
            var result = await appointmentService.GetAppointmentForDeleteAsync(testAppointment.Id, testHairdresserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAppointmentForDeleteAsync_InvalidId_ReturnsNull()
        {
            var result = await appointmentService.GetAppointmentForDeleteAsync(999, testHairdresserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteAppointmentAsync_ValidId_ReturnsTrue()
        {
            var result = await appointmentService.DeleteAppointmentAsync(testAppointment.Id, testHairdresserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteAppointmentAsync_InvalidId_ReturnsFalse()
        {
            var result = await appointmentService.DeleteAppointmentAsync(999, testHairdresserId);

            Assert.IsFalse(result);
        }
    }
}