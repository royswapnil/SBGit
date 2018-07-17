using Newtonsoft.Json.Serialization;
using Owin;
using SterlingBankLMS.Web.Utility;
using System.Web.Http;

namespace SterlingBankLMS.Web
{
    public partial class Startup
	{
        public static void RegisterWebApi(IAppBuilder app)
        {
            // Web API configuration and services
            var config = new HttpConfiguration();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(AppConstants.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional });
            app.UseWebApi(config);
        }
    }
}