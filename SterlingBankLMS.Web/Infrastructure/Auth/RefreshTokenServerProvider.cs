using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;

namespace SterlingBankLMS.Web.Infrastructure.Auth
{
    /// <summary>
    /// Referesh Token Server
    /// </summary>
    public class RefreshTokenServerProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}