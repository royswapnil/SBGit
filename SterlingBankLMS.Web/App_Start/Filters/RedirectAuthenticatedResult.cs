using SterlingBankLMS.Web.Utilities;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.App_Start.Filters
{
    /// <summary>
    /// Redirects an authenticated request to default Page
    /// </summary>
    public class RedirectAuthenticatedRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null) {

                var user = filterContext.HttpContext.User;

                if (user != null) {
                    if (user.Identity.IsAuthenticated) {
                        filterContext.Result = new RedirectResult(AppConstants.DefautltUrl);
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}