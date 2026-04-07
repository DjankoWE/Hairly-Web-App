using Microsoft.AspNetCore.Mvc;
using static Hairly.GCommon.ApplicationConstants.ErrorMessages;

namespace Hairly.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;

            switch (statusCode)
            {
                case 403:
                    ViewData["ErrorMessage"] = AccessDeniedMessage;
                    ViewData["ErrorTitle"] = "Access Denied";
                    return View("AccessDenied");

                case 404:
                    ViewData["ErrorMessage"] = NotFoundMessage;
                    ViewData["ErrorTitle"] = "Page Not Found";
                    return View("NotFound");

                case 500:
                    ViewData["ErrorMessage"] = ServerErrorMessage;
                    ViewData["ErrorTitle"] = "Server Error";
                    return View("ServerError");

                default:
                    ViewData["ErrorMessage"] = GenericErrorMessage;
                    ViewData["ErrorTitle"] = "Error";
                    return View("GenericError");
            }
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View("GenericError");
        }
    }
}
