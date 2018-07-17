using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System;

namespace SterlingBankLMS.Web
{
    public partial class Startup
    {
        //public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        internal static IDataProtectionProvider DataProtectionProvider;

        /// <summary>
        /// Configures Authentication provider middleware
        /// </summary>
        public void ConfigureAuth(IAppBuilder app/*, Container container*/)
        {

            DataProtectionProvider = app.GetDataProtectionProvider();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = AppConstants.AuthenticationType,
                CookieName = AppConstants.AuthenticationType,
                LoginPath = new PathString(AppConstants.AuthProviderEndpoint),
                AuthenticationMode = AuthenticationMode.Active,
                ReturnUrlParameter = "location",
                SlidingExpiration = true, 
#if DEBUG
                ExpireTimeSpan = TimeSpan.FromDays(7),
#else
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
#endif

                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                         validateInterval: TimeSpan.FromMinutes(5),
                         regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                         getUserIdCallback: (claim) => int.Parse(claim.GetUserId())),

                    OnApplyRedirect = context => {
                        if (!IsAjaxRequest(context.Request)) {
                            context.Response.Redirect(context.RedirectUri);
                        }
                    }
                }
            });




            //Func<IUserAccountService> userAccountFactory = () => container.GetInstance<IUserAccountService>();
            //Func<OrganizationFactory> organizationFactory = () => container.GetInstance<OrganizationFactory>();

            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString(AppConstants.Keys.AuthProviderEndpoint),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
            //    Provider = new ApplicationAuthorizationServerProvider("Self", userAccountFactory, organizationFactory),
            //    RefreshTokenProvider = new RefreshTokenServerProvider()
            //};

            //app.UseOAuthBearerTokens(OAuthOptions);
        }


        /// <summary>
        /// Checks if request was sent via XMLHttpRequest
        /// </summary>
        /// <param name="owinRequest"></param>
        /// <returns></returns>
        private static bool IsAjaxRequest(IOwinRequest owinRequest)
        {
            IReadableStringCollection query = owinRequest.Query;

            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest")) {
                return true;
            }

            IHeaderDictionary headers = owinRequest.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }
    }
}