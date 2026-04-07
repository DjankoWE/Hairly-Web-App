using Hairly.Data;
using Hairly.Data.Models;
using Hairly.Data.Models.Enums;
using Hairly.Services.Core;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Review;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hairly.Services.Tests
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private ApplicationDbContext context;
        private IReviewService reviewService;
        private IdentityUser testHairdresserUser;
        private Client testClient;
        private Service testService;
        private Appointment completedAppointment;
        private Appointment scheduledAppointment;
        private Review testReview;
        private string testHairdresserId = "hairdresser-123";
        private string testUserId = "test-user-123";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            SeedTestData();
            reviewService = new ReviewService(context);
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

            context.Users.Add(testHairdresserUser);

            testClient = new Client
            {
                Id = 1,
                FirstName = "Maria",
                LastName = "Ivanova",
                PhoneNumber = "0888999777",
                Email = "maria@test.com",
                UserId = testUserId,
                HairdresserId = testHairdresserId,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            testService = new Service
            {
                Id = 1,
                Name = "Hair Coloring",
                Description = "Professional hair coloring",
                Price = 50.00m,
                DurationInMinutes = 60,
                HairdresserId = testHairdresserId,
                IsDeleted = false
            };

            completedAppointment = new Appointment
            {
                Id = 1,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                HairdresserId = testHairdresserId,
                AppointmentDate = DateTime.Now.AddDays(-3),
                Status = AppointmentStatus.Completed,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            scheduledAppointment = new Appointment
            {
                Id = 2,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                HairdresserId = testHairdresserId,
                AppointmentDate = DateTime.Now.AddDays(5),
                Status = AppointmentStatus.Scheduled,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            testReview = new Review
            {
                Id = 1,
                ClientId = testClient.Id,
                AppointmentId = completedAppointment.Id,
                Rating = 5,
                Comment = "Excellent service!",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            context.Clients.Add(testClient);
            context.Services.Add(testService);
            context.Appointments.Add(completedAppointment);
            context.Appointments.Add(scheduledAppointment);
            context.Reviews.Add(testReview);
            context.SaveChanges();
        }

        [Test]
        public async Task GetAllReviewsAsync_ReturnsReviews()
        {
            var result = await reviewService.GetAllReviewsAsync();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task GetReviewByIdAsync_ValidId_ReturnsReview()
        {
            var result = await reviewService.GetReviewByIdAsync(testReview.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetReviewByIdAsync_InvalidId_ReturnsNull()
        {
            var result = await reviewService.GetReviewByIdAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReviewCreateModelAsync_ValidAppointment_ReturnsModel()
        {
            var newAppointment = new Appointment
            {
                Id = 3,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                HairdresserId = testHairdresserId,
                AppointmentDate = DateTime.Now.AddDays(-5),
                Status = AppointmentStatus.Completed,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
            context.Appointments.Add(newAppointment);
            await context.SaveChangesAsync();

            var result = await reviewService.GetReviewCreateModelAsync(newAppointment.Id, testUserId);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetReviewCreateModelAsync_InvalidAppointment_ReturnsNull()
        {
            var result = await reviewService.GetReviewCreateModelAsync(999, testUserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReviewCreateModelAsync_ScheduledAppointment_ReturnsNull()
        {
            var result = await reviewService.GetReviewCreateModelAsync(scheduledAppointment.Id, testUserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetReviewCreateModelAsync_AlreadyHasReview_ReturnsNull()
        {
            var result = await reviewService.GetReviewCreateModelAsync(completedAppointment.Id, testUserId);

            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateReviewAsync_ValidData_ReturnsTrue()
        {
            var newAppointment = new Appointment
            {
                Id = 4,
                ClientId = testClient.Id,
                ServiceId = testService.Id,
                HairdresserId = testHairdresserId,
                AppointmentDate = DateTime.Now.AddDays(-7),
                Status = AppointmentStatus.Completed,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
            context.Appointments.Add(newAppointment);
            await context.SaveChangesAsync();

            var model = new ReviewCreateViewModel
            {
                AppointmentId = newAppointment.Id,
                Rating = 4,
                Comment = "Very good service"
            };

            var result = await reviewService.CreateReviewAsync(model, testUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateReviewAsync_ScheduledAppointment_ReturnsFalse()
        {
            var model = new ReviewCreateViewModel
            {
                AppointmentId = scheduledAppointment.Id,
                Rating = 5,
                Comment = "Great!"
            };

            var result = await reviewService.CreateReviewAsync(model, testUserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task CreateReviewAsync_DuplicateReview_ReturnsFalse()
        {
            var model = new ReviewCreateViewModel
            {
                AppointmentId = completedAppointment.Id,
                Rating = 5,
                Comment = "Trying to add second review"
            };

            var result = await reviewService.CreateReviewAsync(model, testUserId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetReviewDeleteModelAsync_ValidId_ReturnsModel()
        {
            var result = await reviewService.GetReviewDeleteModelAsync(testReview.Id);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetReviewDeleteModelAsync_InvalidId_ReturnsNull()
        {
            var result = await reviewService.GetReviewDeleteModelAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task DeleteReviewAsync_UserOwnsReview_ReturnsTrue()
        {
            var result = await reviewService.DeleteReviewAsync(testReview.Id, testUserId, false);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteReviewAsync_AdminDeletesAnyReview_ReturnsTrue()
        {
            var result = await reviewService.DeleteReviewAsync(testReview.Id, "admin-user-999", true);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteReviewAsync_UserDoesntOwnReview_ReturnsFalse()
        {
            var result = await reviewService.DeleteReviewAsync(testReview.Id, "other-user-789", false);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteReviewAsync_InvalidId_ReturnsFalse()
        {
            var result = await reviewService.DeleteReviewAsync(999, testUserId, false);

            Assert.IsFalse(result);
        }
    }
}