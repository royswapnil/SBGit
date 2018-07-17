using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SterlingBankLMS.Web.Startup))]

namespace SterlingBankLMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            UseSimpleInjector();
            ConfigureAuth(app);
            RegisterWebApi(app);

            // remove mvc header
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}