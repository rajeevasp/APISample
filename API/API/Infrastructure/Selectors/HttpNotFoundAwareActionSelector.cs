using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;
using API.Controllers;

namespace API.Infrastructure.Selectors
{
    /// <summary>
    /// Handle 404 request to invalid actions.
    /// </summary>
    public class HttpNotFoundAwareActionSelector : ApiControllerActionSelector
    {
        /// <summary>
        /// Initialize the <see cref="HttpNotFoundAwareActionSelector"/> class
        /// </summary>
        public HttpNotFoundAwareActionSelector()
        {
        }

        /// <summary>
        /// Catch requests to invalid actions and re-route to the error controller
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <returns>HttpActionDescriptor</returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor descriptor = null;
            try
            {
                descriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                {
                    throw;
                }
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "handle404";
                IHttpController errorController = new ErrorController();
                controllerContext.Controller = errorController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(
                    controllerContext.Configuration, "Error", typeof(ErrorController));
                descriptor = base.SelectAction(controllerContext);
            }
            return descriptor;
        }
    }
}