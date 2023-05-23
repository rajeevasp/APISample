using System.Web.Http.Controllers;
using API.Infrastructure.Attributes;

namespace API.Infrastructure.Extensions
{
    public static class HttpControllerContextExtensions
    {
        /// <summary>
        /// Checks if the controller is secured, ie if the <see cref="RequireSignatureAttribute"/>
        /// is present on the controller.
        /// </summary>
        /// <param name="context"><see cref="HttpControllerContext"/></param>
        /// <returns>bool Secured or not</returns>
        public static bool IsSecured(this HttpControllerContext context)
        {
            return context.ControllerDescriptor.GetCustomAttributes<RequireSignatureAttribute>().Count == 1;
        }
    }
}