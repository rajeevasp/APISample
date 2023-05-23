using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Domain.Services;

namespace API.Infrastructure.Attributes
{
    /// <summary>
    /// Validate signatures on endpoints that need security
    /// </summary>
    public class RequireSignatureAttribute : ActionFilterAttribute
    {
        private IApiService apiService;
        private const string AuthorizationHeaderName = "X-UW-Authorization";
        private string reason = string.Empty;

        /// <summary>
        /// Check if the request has a valid signature before responding.
        /// </summary>
        /// <param name="actionContext"><see cref="HttpActionContext"/></param>
        public override async void OnActionExecuting(HttpActionContext actionContext)
        {
            apiService = actionContext.ActionDescriptor.Configuration.DependencyResolver.GetService(typeof(IApiService)) as IApiService;

            if (!(await IsAuthenticated(actionContext.Request))) 
            {
                //log blocked request
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(reason);
                actionContext.Response = response;
            }
        }

        /// <summary>
        /// Checks if the request contains a valid request signature
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/></param>
        /// <returns>bool Authenticated</returns>
        private async Task<bool> IsAuthenticated(HttpRequestMessage request)
        {
            string requestSignature = ExtractSignature(request.Headers, request.RequestUri);
            if (string.IsNullOrEmpty(requestSignature))
            {
                reason = "Empty request signature.";
                return false;
            }

            if (DuplicateRequest(requestSignature))
            {
                reason = "Request has previously been sent.";
                return false;
            }

            if (!request.Headers.Date.HasValue)
            {
                reason = "Empty Http Date Header";
                return false;
            }

            var timestamp = request.Headers.Date.Value.UtcDateTime;
            if (!ValidTimestamp(timestamp))
            {
                reason = "Invalid request.";
                return false;
            }
            
            var baseString = await CreateStringToSign(request, timestamp);
            var generatedSignature = await apiService.CreateSignature(baseString, request.RequestUri.ParseQueryString()["key"]);

            if (string.IsNullOrEmpty(generatedSignature) || !generatedSignature.Equals(requestSignature))
            {
                reason = "Invalid signature.";
                return false;
            }

            AddToCache(requestSignature);
            return true;
        }

        /// <summary>
        /// Checks that the request time is a maximum 2mins greater or lesser 
        /// than the current coordinated universal time.
        /// </summary>
        /// <param name="timestamp"><see cref="DateTime"/></param>
        /// <returns>bool valid request timestamp</returns>
        private bool ValidTimestamp(DateTime timestamp)
        {
            var utcNow = DateTime.UtcNow;
            if (timestamp >= utcNow.AddMinutes(5) || timestamp <= utcNow.AddMinutes(-5))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Creates the string to sign in teh correct format, consisting of:
        /// {Http Method}\n
        /// {Uri}\n
        /// {Parameter-String}\n
        /// {Api-key}
        /// {timestamp Format RFC 1123}
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/></param>
        /// <param name="requestDate">UTC Request time</param>
        /// <returns>String to sign</returns>
        private async Task<string> CreateStringToSign(HttpRequestMessage request, DateTime requestDate)
        {
            string method = request.Method.Method;
            string uri = request.RequestUri.GetLeftPart(UriPartial.Path);
            string parameterString = await BuildOrderedParameterString(request);
            string apiKey = request.RequestUri.ParseQueryString()["key"];
            string timestamp = Uri.EscapeDataString(requestDate.ToString("r"));
            return string.Join("\n", new[] { method, uri, parameterString, apiKey, timestamp });
        }

        /// <summary>
        /// Orders the parameter string
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/></param>
        /// <returns>Ordered parameter string</returns>
        private Task<string> BuildOrderedParameterString(HttpRequestMessage request)
        {
            var queryStringCollection = request.RequestUri.ParseQueryString();
            var sortedList = new SortedDictionary<string, string>(
                queryStringCollection.AllKeys.ToDictionary(d => d,
                                                           d => queryStringCollection[d])
            ).Select(s => string.Format("{0}={1}", Uri.EscapeDataString(s.Key), Uri.EscapeDataString(s.Value)));
            return Task.FromResult(string.Join("&", sortedList));
        }

        /// <summary>
        /// If the memory cache contains the signature then this is a second request
        /// and is invalid.
        /// </summary>
        /// <param name="signature">Request signature</param>
        /// <returns></returns>
        private static bool DuplicateRequest(string signature)
        {
            if (MemoryCache.Default.Contains(signature))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the signature to the memory cache.
        /// </summary>
        /// <param name="signature">Request signature</param>
        private static void AddToCache(string signature)
        {
            var memoryCache = MemoryCache.Default;
            if (!memoryCache.Contains(signature))
            {
                var expiration = DateTimeOffset.Now.AddMinutes(5);
                memoryCache.Add(signature, signature, expiration);
            }
        }

        /// <summary>
        /// Gets the signature value from the request
        /// </summary>
        /// <param name="requestUri"><see cref="Uri"/></param>
        /// <param name="headers">Request <see cref="HttpHeaders"/></param>
        /// <returns>Request string signature</returns>
        private static string ExtractSignature(HttpHeaders headers, Uri requestUri)
        {
            var authHeader = GetHttpHeader(headers, AuthorizationHeaderName);
            if (!string.IsNullOrEmpty(authHeader))
            {
                return authHeader;
            }
            return string.Empty;
        }

        /// <summary>
        /// Helper method to retrieve a http header by its name
        /// </summary>
        /// <param name="headers">Request <see cref="HttpHeaders"/></param>
        /// <param name="name">Header name</param>
        /// <returns>Header string</returns>
        private static string GetHttpHeader(HttpHeaders headers, string name)
        {
            if (!headers.Contains(name))
            {
                return string.Empty;
            }

            return headers.GetValues(name)
                .SingleOrDefault();
        }
    }
}