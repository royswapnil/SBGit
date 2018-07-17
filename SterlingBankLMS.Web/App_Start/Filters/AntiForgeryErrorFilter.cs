using System.Web.Mvc;

namespace SterlingBankLMS.Web.App_Start.Filters
{
    public sealed class AntiForgeryErrorFilter : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Redirects an <see cref="HttpAntiForgeryException"/>  to an error page
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is HttpAntiForgeryException) {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.TrySkipIisCustomErrors = true;
                context.HttpContext.Response.StatusCode = 400;

                context.Result = new ViewResult
                {
                    ViewName = "~/views/error/badrequest.cshtml",
                };

                context.ExceptionHandled = true;
            }
        }
    }
}