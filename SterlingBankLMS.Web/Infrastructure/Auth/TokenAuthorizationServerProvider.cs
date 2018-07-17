using Microsoft.Owin.Security.OAuth;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using System;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Auth.Provider
{
    /// <summary>
    /// OAuth access token Authorization Provider
    /// </summary>
    public class TokenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _clientId;

        Func<IUserAccountService> userAcctServiceFactory;
        Func<OrganizationFactory> organizationFactory;
        private IUserAccountService UseraccountService
        {
            get
            {
                return userAcctServiceFactory.Invoke();
            }
        }

        private OrganizationFactory OrganizationFactory
        {
            get
            {
                return organizationFactory.Invoke();
            }
        }

        public TokenAuthorizationServerProvider(string clientId,
            Func<IUserAccountService> _userAcctServiceFactory,
            Func<OrganizationFactory> _organizationFactory)
        {
            _clientId = clientId;
            userAcctServiceFactory = _userAcctServiceFactory;
            organizationFactory = _organizationFactory;
        }

        /// <summary>
        /// Validates Client making an Oauth call
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => {
                return context.Validated();
            });
        }

        /// <summary>
        /// Validate User account against DB or Active Directory Service 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
        }


        private ApplicationUser GetRegisteredUserAccount(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return UseraccountService.ValidateUser(context.UserName, context.Password);
        }
    }
}