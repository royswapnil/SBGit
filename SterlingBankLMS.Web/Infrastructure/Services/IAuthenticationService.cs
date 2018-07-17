using System.Security.Claims;
using Microsoft.Owin.Security;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    /// <summary>
    /// Handles User Claims Authentication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Add User ClaimIdentities to a request
        /// </summary>
        /// <param name="identities"></param>
        void SignIn(params ClaimsIdentity[] identities);

        /// <summary>
        /// Add User ClaimIdentities to a request
        /// </summary>
        /// <param name="identities"></param>
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);

        /// <summary>
        /// Revokes User ClaimIdentities from a request
        /// </summary>
        /// <param name="identities"></param>
        void SignOut(params string[] authenticationTypes);
    }
}