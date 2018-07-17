using SterlingBankLMS.Web.ViewModels;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Utilities
{
    public class AjaxErrorHandler : FilterAttribute, IExceptionFilter
    {
 
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.ExceptionHandled = true;
                filterContext.RequestContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new JsonResult
                {
                    Data = new ApiResult<object>
                    {
                        Message = "An error occurred. Please check if you are connected to the internet",
                        HasError = true
                    }

                //Data = new { errorMessage = "some error message" }
                };
            }
        }
    }
}