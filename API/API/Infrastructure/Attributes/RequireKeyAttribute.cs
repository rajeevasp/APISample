using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Domain.Services;
using API.Infrastructure.Extensions;

namespace API.Infrastructure.Attributes
{
    /// <summary>
    /// Checks for a api key present in the 
    /// </summary>
    public class RequireKeyAttribute : ActionFilterAttribute
    {
        private IApiService apiService;

        /// <summary>
        /// Checks the request has a valid API key
        /// </summary>
        /// <param name="actionContext"><see cref="HttpActionContext"/></param>
        public override async void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            if (!actionContext.IsSecured() && !actionContext.ControllerContext.IsSecured()) 
            {
                apiService = request.GetDependencyScope().GetService(typeof(IApiService)) as IApiService;

                string key = ExtractApiKey(request);
                string reason = string.Empty;
                string apiName = Assembly.GetExecutingAssembly().GetName().Name;

                var result = await apiService.ValidateApiKey(key, apiName);
                if (string.IsNullOrEmpty(key) || !result.Item1)
                {
                    reason = result.Item2;
                    if (string.IsNullOrEmpty(reason))
                    {
                        reason = "Empty API key.";
                    }
                    var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    response.Content = new StringContent(reason);
                    actionContext.Response = await Task.FromResult(response);
                }
            }
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// Trys to get a api key from the querystring
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/></param>
        /// <returns>string api key</returns>
        private string ExtractApiKey(HttpRequestMessage request)
        {
            Uri uri = request.RequestUri;
            var key = uri.ParseQueryString()["key"];
            if (!string.IsNullOrEmpty(key)) 
            {
                return key;
            }
            return string.Empty;
        }
    }
}