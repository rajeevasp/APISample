using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using API.Infrastructure.Attributes;

namespace API.Infrastructure.Extensions
{
    public static class HttpActionContextExtensions
    {
        /// <summary>
        /// Checks if the current action is secured, ie has a <see cref="RequireSignatureAttribute"/>
        /// attribute added.
        /// </summary>
        /// <param name="context"><see cref="HttpActionContext"/></param>
        /// <returns>bool secured or not</returns>
        public static bool IsSecured(this HttpActionContext context)
        {
            return context.ActionDescriptor.GetCustomAttributes<RequireSignatureAttribute>().Count == 1;
        }

    }
}