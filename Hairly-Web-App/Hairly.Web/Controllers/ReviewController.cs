using System.Security.Claims;
using Hairly.Services.Core.Contracts;
using Hairly.Web.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants;
using static Hairly.GCommon.ApplicationConstants.SuccessMessages;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;

namespace Hairly.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reviews = await reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var review = await reviewService.GetReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create(int appointmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var model = await reviewService.GetReviewCreateModelAsync(appointmentId, userId);

            if (model == null)
            {
                TempData[ErrorMessageKey] = ReviewNotAllowed;
                return RedirectToAction("Index", "Appointment");
            }
           
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            bool isCreated = await reviewService.CreateReviewAsync(model, userId);

            if (isCreated)
            {
                TempData[SuccessMessageKey] = ReviewCreatedSuccessfully;
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, ReviewCreateError);
            return View(model);
        }
    }
}
