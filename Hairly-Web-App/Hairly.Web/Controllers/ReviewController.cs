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
    }
}
