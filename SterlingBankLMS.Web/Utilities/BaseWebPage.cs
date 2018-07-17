using SterlingBankLMS.Web.Infrastructure.Auth;
using System.Security.Claims;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Utilities
{
    public abstract class BaseWebPage : BaseWebPage<dynamic> { }

    public abstract class BaseWebPage<TModel> : WebViewPage<TModel>
    {
        public UserClaims CurrentUser { get { return new UserClaims(base.User as ClaimsPrincipal); } }
    }
}