using Elmah;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Infrastructure.Attributes
{
    /// <summary>
    /// Globally handle exceptions
    /// </summary>
    public class ExceptionHandler : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //object outValue = null;
            //actionExecutedContext.Response.TryGetContentValue<object>(out outValue);
            if (actionExecutedContext.Response == null)
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionExecutedContext.Exception.InnerException.InnerException.Message);
            }

        }
    }



}