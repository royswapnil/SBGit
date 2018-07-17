using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SterlingBankLMS.Web.Models.IdentityModels;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    /// <summary>
    /// Application Signin Manager
    /// </summary>
    public class ApplicationSignInManager : SignInManager<ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}