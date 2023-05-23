using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using API.Infrastructure.Results;

namespace API.Infrastructure.Extensions
{
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Generate a friendly message to the client when we cant find records
        /// </summary>
        /// <param name="controller"><see cref="ApiController"/></param>
        /// <param name="message">Optional message to send to client</param>
        /// <returns><see cref="RecordNotFoundResult"/></returns>
        public static RecordNotFoundResult RecordNotFound(this ApiController controller, string message = "")
        {
            return new RecordNotFoundResult(controller.Request, message);
        }   
    }
}