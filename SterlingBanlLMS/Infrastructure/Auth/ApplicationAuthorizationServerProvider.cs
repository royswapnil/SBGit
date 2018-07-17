using Microsoft.Owin.Security.OAuth;
using System;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Auth.Provider
{
    /// <summary>
    /// Application Authorization Provider
    /// </summary>
    public class ApplicationAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {
        private readonly string _clientId;
        public ApplicationAuthorizationServerProvider(string clientId)
        {
            _clientId = clientId;
        }

        /// <summary>
        /// Validates Client making an OAuth call
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validate C
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            throw new NotImplementedException();
        }
    }
}