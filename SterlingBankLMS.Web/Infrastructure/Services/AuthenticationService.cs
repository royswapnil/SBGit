using Microsoft.Owin.Security;
using SterlingBankLMS.Web.Infrastructure.Auth;
using System.Security.Claims;

namespace SterlingBankLMS.Web.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationSignInManager _authManager;
        public AuthenticationService(ApplicationSignInManager authManager)
        {
            _authManager = authManager;
        }
        protected virtual void SignIn(params ClaimsIdentity[] identities)
        {
            _authManager.AuthenticationManager.SignIn(identities);
        }

      
        void IAuthenticationService.SignIn(params ClaimsIdentity[] identities)
        {
            SignIn(identities);
        }

        
        void IAuthenticationService.SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            SignIn(properties, identities);
        }

        protected virtual void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            _authManager.AuthenticationManager.SignIn(properties, identities);
        }


        protected virtual void SignOut(params string[] authenticationTypes)
        {
            _authManager.AuthenticationManager.SignOut(authenticationTypes);
        }

        void IAuthenticationService.SignOut(params string[] authenticationTypes)
        {
            SignOut(authenticationTypes);
        }
    }
}