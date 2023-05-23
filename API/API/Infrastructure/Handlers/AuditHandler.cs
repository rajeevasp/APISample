using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Domain.Infrastructure;
using API.Domain.Repository;
using API.Utilities.Extensions;

namespace API.Infrastructure.Handlers
{
    public class AuditHandler : DelegatingHandler
    {
        private IApiLogRecords apiLog;

        /// <summary>
        /// Log requests and response
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            Stopwatch stopwatch = null;
            Exception exception = null;

            try
            {
                stopwatch = Stopwatch.StartNew();
                response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            apiLog = request.GetDependencyScope().GetService(typeof(IApiLogRecords)) as IApiLogRecords;
            await Task.Run(() => Log(request, response, stopwatch.ElapsedMilliseconds, exception));
            return response;
        }

        /// <summary>
        /// Logs a request/response to the database.
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="response">Response to send</param>
        /// <param name="responseTime">Time taken to send the response (milliseconds)</param>
        /// <param name="exception">Optional exception</param>
        private async void Log(HttpRequestMessage request, HttpResponseMessage response, long responseTime, Exception exception = null)
        {

            try
            {
                string ipAddress = string.Empty;
                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    HttpContextWrapper context = (HttpContextWrapper)request.Properties["MS_HttpContext"];
                    if (context != null)
                    {
                        ipAddress = context.Request.UserHostAddress;
                    }
                }

                EndpointLogItem log = new EndpointLogItem();
                log.Application = Assembly.GetExecutingAssembly().GetName().Name;
                log.Exception = exception.GetExceptionDetails();
                log.HttpMethod = request.Method.Method;
                log.HttpVersion = request.Version.ToString();
                log.IpAddress = ipAddress;
                log.BytesReceived = await GetBytesReceived(request);
                log.BytesSent = await GetBytesSent(response);
                log.MachineName = Environment.MachineName;
                log.RequestUri = request.RequestUri.AbsoluteUri;
                log.ApiKey = GetApiKey(request.RequestUri);
                log.ResponseTime = responseTime;
                log.StatusCode = (int)response.StatusCode;
                log.Timestamp = DateTime.UtcNow;
                log.JsonRecord = log.FillJsonRecord().ToString();

                //Persist to data store
                apiLog.AddLog(log);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        /// <summary>
        /// Gets the api key from query string
        /// </summary>
        /// <param name="uri"><see cref="Uri"/></param>
        /// <returns>string</returns>
        private string GetApiKey(Uri uri)
        {
            var values = HttpUtility.ParseQueryString(uri.Query);
            if (values.Get("key") != null)
            {
                return values.Get("key");
            }
            return string.Empty;
        }

        /// <summary>
        /// Calculates the kilobytes sent with the response
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/></param>
        /// <returns>Kilobytes</returns>
        private async Task<long> GetBytesSent(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return responseStream.Length;
            }
            return 0;
        }

        /// <summary>
        /// Calculates the kilobytes received in the request
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/></param>
        /// <returns>Kilobytes</returns>
        private async Task<long> GetBytesReceived(HttpRequestMessage request)
        {
            if (request.Method.Method == "POST")
            {
                var requestStream = await request.Content.ReadAsStreamAsync();
                return requestStream.Length;
                
            }
            return 0;
        }
    }
}