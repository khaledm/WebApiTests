using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using WebApiTests.Exceptions;

namespace WebApiTests.Filters
{
    /// <inheritdoc />
    public class ModelBindingExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ModelBindingException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}