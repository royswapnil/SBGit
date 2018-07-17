using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SterlingBankLMS.Web.Api
{
    /// <summary>
    /// Base API with basic utilities
    /// </summary>
    public abstract class BaseApiController : ApiController
    {

        /// <summary>
        /// Returns an <see cref="HttpResponseMessage"/> with status 403.
        /// </summary>
        /// <returns></returns>
        protected internal IHttpActionResult AccessDeniedResult()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                ReasonPhrase = "Sorry, you have no access to this resource. Please contact your Admin."
            };
            return ResponseMessage(response);
        }

        /// <summary>
        /// Returns an <see cref="HttpResponseMessage"/> with status code 404
        /// </summary>
        /// <returns></returns>
        protected internal IHttpActionResult NotFoundResult(string message = null)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                ReasonPhrase = message ?? "Resource not found."
            };
            return ResponseMessage(response);
        }
    }
}