using DataTables.AspNet.WebApi2;
using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using Rentrdoid.Common.Logger;
//# if !DEBUG
using SterlingBankLMS.Web.Controllers;
//#endif
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace SterlingBankLMS.Web
{
    public class MvcApplication : HttpApplication
    {

        protected void Application_Start()
        {

           // Migrations.Configuration.InitializeDb();

            var binder = new ModelBinder();
            binder.ParseAdditionalParameters = Parser;
            GlobalConfiguration.Configuration.RegisterDataTables(Parser, false);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.Add(new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()));
        }
        private IDictionary<string, object> Parser(HttpActionContext controllerContext, System.Web.Http.ModelBinding.ModelBindingContext modelBindingContext)
        {
            var output = new Dictionary<string, object>();
            if (modelBindingContext.ValueProvider.GetValue("AdditionalParameters") != null)
            {
                var p1 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("AdditionalParameters").AttemptedValue);

                output["data"] = p1;

            }
            return output;

        }
        protected void Application_EndRequest()
        {
            Response.Headers.Remove("Server");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            LogException(exception);
//#if !DEBUG
            var httpException = exception as HttpException;
            var exceptionCode = httpException?.GetHttpCode() ?? 0;

            Response.Clear();
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            IController errorController = DependencyResolver.Current.GetService<ErrorController>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "error");

            if (exceptionCode == 404) {
                routeData.Values.Add("action", "pagenotfound");
            }
            else if (exceptionCode == 500) {
                routeData.Values.Add("action", "servererror");
            }
            else {
                routeData.Values.Add("action", "servererror");
            }

            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
//#endif
        }

        private void LogException(Exception exception)
        {
            var logger = DependencyResolver.Current.GetService<ILogger>();
            logger.Error(exception, exception.Message);
        }
    }
}