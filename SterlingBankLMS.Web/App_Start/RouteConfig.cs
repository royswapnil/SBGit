using System.Web.Mvc;
using System.Web.Routing;

namespace SterlingBankLMS.Web
{
    /// <summary>
    /// Configure MVC Routes
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("accessdenied", "authentication/accessdenied",
                        new
                        {
                            controller = "authentication",
                            action = "accessdenied"
                        });

            routes.MapRoute("notfound", "error/notfound",
                new
                {
                    controller = "error",
                    action = "notfound"
                });

            routes.MapRoute("passwordresetstatus", "authentication/passwordresetstatus",
                new
                {
                    controller = "authentication",
                    action = "passwordresetstatus"
                });

            routes.MapRoute(
                name: "Home",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            ).DataTokens = new RouteValueDictionary(new { area = "common" });
        }
    }
}