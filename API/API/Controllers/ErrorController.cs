using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Infrastructure.Attributes;

namespace API.Controllers
{
    /// <summary>
    /// Controller to return 404 responses
    /// </summary>
    [HideFromDocumentation]
    public class ErrorController : ApiController
    {
        /// <summary>
        /// Send a not found response to the client
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        [HttpGet, HttpPost, HttpPut, HttpPatch, HttpOptions, HttpHead, HttpDelete]
        public HttpResponseMessage Handle404()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.ReasonPhrase = "The requested resource could not be found.";
            return response;
        }

    }
}
