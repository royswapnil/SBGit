using System.Net;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Controllers
{
    /// <summary>
    /// Error Controller
    /// </summary>

    [AllowAnonymous]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Page not found
        /// </summary>
        /// <returns></returns>
        public ActionResult PageNotFound()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return View();
        }

        /// <summary>
        /// Server error
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerError()
        {
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            return View();
        }
    }
}