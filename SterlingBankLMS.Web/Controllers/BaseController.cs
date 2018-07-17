using SterlingBankLMS.Web.Utilities;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Controllers
{
    /// <summary>
    /// base controller for shared functionality accross controllers
    /// </summary>
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Redirects to <paramref name="location"/> or Home Page if no parameter was supplied
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        protected ActionResult RedirectOrHome(string location = null)
        {
            if (string.IsNullOrWhiteSpace(location) || !Url.IsLocalUrl(location))
                return Redirect(AppConstants.DefautltUrl);

            return Redirect(location);
        }

        /// <summary>
        /// Redirects to access denied view
        /// </summary>
        /// <returns></returns>
        protected ActionResult AccessDeniedView()
        {
            return RedirectToAction("accessdenied", "authentication", new { area = "" });
        }

        /// <summary>
        /// Redirects to notfound
        /// </summary>
        /// <returns></returns>
        protected ActionResult NotFoundView()
        {
            return RedirectToAction("notfound", "error", new { area = "" });
        }
    }
}