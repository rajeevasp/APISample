using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace API.Infrastructure
{
    /// <summary>
    /// Request message extensions
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Checks if the request is a KML request. Checks the accept header
        /// and then looks for a fmt parameter with a kml value
        /// </summary>
        /// <param name="request">Current request</param>
        /// <returns>bool kml request or not</returns>
        public static bool IsKMLRequest(this HttpRequestMessage request)
        {
            string kmlMediaType = "application/vnd.google-earth.kml+xml";

            if (request.Headers.Accept.Contains(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(kmlMediaType)))
            {
                return true;
            }

            var fmtParameter = request.RequestUri.ParseQueryString().Get("fmt");

            if (!string.IsNullOrEmpty(fmtParameter)) 
            {
                return fmtParameter.Equals("kml", StringComparison.OrdinalIgnoreCase);  
            }

            return false;
        }
    }
}