using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Infrastructure.Formatters;

namespace API.Infrastructure.Attributes
{
    /// <summary>
    /// Attribute to add KML support on the fly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SupportKMLAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Add the KML formatter on the fly
        /// </summary>
        /// <param name="actionContext">Current request context</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var kmlFormatter = new KMLFormatter();
            actionContext.ControllerContext.Configuration.Formatters.Add(kmlFormatter);
            base.OnActionExecuting(actionContext);
        }
    }
}