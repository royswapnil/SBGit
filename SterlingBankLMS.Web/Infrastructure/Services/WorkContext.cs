using SterlingBankLMS.Web.Infrastructure.Auth;
using System.Security.Claims;
using System.Web;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public interface IWorkContext
    {
        UserClaims User { get; }
    }

    public class WorkContext : IWorkContext
    {
        private readonly HttpContextBase _httpCtxt;

        public WorkContext(HttpContextBase httpCtxt)
        {
            _httpCtxt = httpCtxt;
        }

        public UserClaims User
        {
            get
            {
                if (_httpCtxt.User.Identity != null && _httpCtxt.User.Identity.IsAuthenticated) {
                    return new UserClaims(_httpCtxt.User as ClaimsPrincipal);
                }

                return null;
            }
        }
    }
}