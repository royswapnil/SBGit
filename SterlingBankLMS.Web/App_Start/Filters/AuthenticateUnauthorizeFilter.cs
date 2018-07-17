using System;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.App_Start.Filters
{
    /// <summary>
    /// Implement the methods that are required for an authorization filter.
    /// </summary>
    public class AuthenticateUnauthorizeFilter : IAuthorizationFilter
    {
        public AuthenticateUnauthorizeFilter()
        {
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                                   || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuthorization) {
                return;
            }

            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated)
                filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}