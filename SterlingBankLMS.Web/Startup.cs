using Microsoft.Owin;
using Owin;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(SterlingBankLMS.Web.Startup))]

namespace SterlingBankLMS.Web
{
    /// <summary>
    /// Owin Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configure Owin 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var container = UseSimpleInjector();
            ConfigureAuth(app);
            UseAutomapper();

            // disable mvc header
            MvcHandler.DisableMvcResponseHeader = true;

            //Map SignalR
            app.MapSignalR();
        }
    }
}