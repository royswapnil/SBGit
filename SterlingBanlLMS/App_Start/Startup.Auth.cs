using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SterlingBankLMS.Web.Infrastructure.Auth.Provider;
using SterlingBankLMS.Web.Utility;
using System;

namespace SterlingBankLMS.Web
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString(AppConstants.Keys.AuthProviderEndpoint),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new ApplicationAuthorizationServerProvider("Self"),
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}