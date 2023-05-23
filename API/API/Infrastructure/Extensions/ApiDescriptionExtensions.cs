using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http;
using System.Web.Routing;

namespace API.Infrastructure.Extensions
{
    public static class ApiDescriptionExtensions 
    {
        /// <summary>
        /// Creates a default Uri string with parameters added via the querystring
        /// </summary>
        /// <param name="description"><see cref="ApiDescription"/></param>
        /// <param name="includeQuerystring">Whether to include the querystring in the response</param>
        /// <returns>Uri String</returns>
        public static string GetDefaultUri(this ApiDescription description, bool includeQuerystring = true)
        {
            var uri = BuildDefaultUri(description);

            if (includeQuerystring)
            {
                if (description.ParameterDescriptions.Count > 0)
                {
                    StringBuilder querystring = new StringBuilder("?");
                    foreach (var parameter in description.ParameterDescriptions)
                    {
                        querystring.AppendFormat("{0}={{{1}}}&", parameter.Name, parameter.Name);
                    }
                    return string.Format("{0}{1}", uri, querystring.ToString().TrimEnd('&'));
                }
            }
            return uri;
        }

        /// <summary>
        /// Checks whether the action has a attribute route or not. Compares the relative root
        /// against the default URI we build.
        /// </summary>
        /// <param name="description"><see cref="ApiDescription"/></param>
        /// <returns>Has attribtue route or not </returns>
        public static bool HasAttributeRoute(this ApiDescription description)
        {
            var defaultUri = description.GetDefaultUri(true);
            if (defaultUri.Equals(description.RelativePath, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the route attribute uri. Often a friendly url to request and only
        /// includes the required parameters
        /// </summary>
        /// <param name="description"><see cref="ApiDescription"/></param>
        /// <returns>string</returns>
        public static string GetAttributeRoute(this ApiDescription description)
        {
            var relativePath = description.RelativePath;
            var startIndex = relativePath.IndexOf("?");
            if (startIndex == -1)
            {
                return relativePath;
            }
            return relativePath.Substring(0, startIndex);
        }

        /// <summary>
        /// Generates an URI-friendly ID for the <see cref="ApiDescription"/>. E.g. "Get-Values-id_name" instead of "GetValues/{id}?name={name}"
        /// </summary>
        /// <param name="description">The <see cref="ApiDescription"/>.</param>
        /// <returns>The ID as a string.</returns>
        public static string GetFriendlyId(this ApiDescription description)
        {
            string path = description.RelativePath;
            string[] urlParts = path.Split('?');
            string localPath = urlParts[0];
            string queryKeyString = null;
            if (urlParts.Length > 1)
            {
                string query = urlParts[1];
                string[] queryKeys = HttpUtility.ParseQueryString(query).AllKeys;
                queryKeyString = String.Join("_", queryKeys);
            }

            StringBuilder friendlyPath = new StringBuilder();
            friendlyPath.AppendFormat("{0}-{1}",
                description.HttpMethod.Method,
                localPath.Replace("/", "-").Replace("{", String.Empty).Replace("}", String.Empty));
            if (queryKeyString != null)
            {
                friendlyPath.AppendFormat("_{0}", queryKeyString);
            }
            return friendlyPath.ToString();
        }

        #region Helpers 

        /// <summary>
        /// Builds a URI from the default route and action/controller descriptors.
        /// </summary>
        /// <param name="description"><see cref="ApiDescription"/></param>
        /// <returns>URI String</returns>
        private static string BuildDefaultUri(ApiDescription description)
        {
            var defaultRouteTemplate = description.ActionDescriptor.Configuration.Routes["DefaultApi"];
            var action = description.ActionDescriptor.ActionName;
            var controller = description.ActionDescriptor.ControllerDescriptor.ControllerName;
            return defaultRouteTemplate.RouteTemplate
                .Replace("{controller}", controller)
                .Replace("{action}", action)
                .ToLower();
        }

        #endregion
    }
}