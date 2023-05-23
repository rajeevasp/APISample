using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace API.Infrastructure.Selectors
{
    /// <summary>
    /// Handle 404 request to invalid controllers
    /// </summary>
    public class HttpNotFoundAwareControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// Initialize the <see cref="HttpNotFoundAwareControllerSelector"/> class
        /// </summary>
        /// <param name="config"><see cref="HttpConfiguraton"/></param>
        public HttpNotFoundAwareControllerSelector(HttpConfiguration config)
            : base (config)
        {
        }

        /// <summary>
        /// Perform the standard controller selecting but, catch not found requests
        /// and reroute to the error controller 404 action
        /// </summary>
        /// <param name="request">Current Request</param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor descriptor = null;
            try
            {
                descriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                var statusCode = ex.Response.StatusCode;
                if (statusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "error";
                routeValues["action"] = "handle404";
                descriptor = base.SelectController(request);
            }
            return descriptor;
        }
    }
}