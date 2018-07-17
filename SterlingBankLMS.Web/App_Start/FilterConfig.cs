using SterlingBankLMS.Web.App_Start.Filters;
using System.Web.Mvc;

namespace SterlingBankLMS.Web
{
    /// <summary>
    /// Filter Config
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Register Global Filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthenticateUnauthorizeFilter());
            filters.Add(new AntiForgeryErrorFilter());
        }
    }
}