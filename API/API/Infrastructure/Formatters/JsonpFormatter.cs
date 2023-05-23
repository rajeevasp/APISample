using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace API.Infrastructure.Formatters
{
    /// <summary>
    /// Support JSONP response formats
    /// </summary>
    public class JsonpFormatter : MediaTypeFormatter
    {
        private readonly MediaTypeFormatter jsonMediaTypeFormatter;
        private readonly HttpRequestMessage request;
        private readonly string callbackParameterName;
        private readonly string callbackParameterValue;

        /// <summary>
        /// Initialize a new instance of the <see cref="JsonpFormatter"/> class
        /// </summary>
        /// <param name="jsonMediaTypeFormatter">Standard json media formatter</param>
        /// <param name="callbackParameterName">Callback parameter name</param>
        public JsonpFormatter(MediaTypeFormatter jsonMediaTypeFormatter, string callbackParameterName = "callback")
        {
            if (jsonMediaTypeFormatter == null)
            {
                throw new ArgumentNullException("JsonMediaTypeFormatter instance cannot be null");
            }

            this.jsonMediaTypeFormatter = jsonMediaTypeFormatter;
            this.callbackParameterName = callbackParameterName;

            foreach (var encoding in this.jsonMediaTypeFormatter.SupportedEncodings)
            {
                SupportedEncodings.Add(encoding);
            }

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            //this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
            //this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
            //this.MediaTypeMappings.Add(new QueryStringMapping("fmt", "kml", "application/vnd.google-earth.kml+xml"));
            //Need to find a way to set this for jsonp requests
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));
            MediaTypeMappings.Add(new QueryStringMapping("fmt", "json", "application/json"));
            MediaTypeMappings.Add(new QueryStringMapping("fmt", "jsonp", "application/json"));
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="JsonpFormatter"/> class
        /// </summary>
        /// <param name="jsonMediaTypeFormatter">Standard </param>
        /// <param name="request"></param>
        /// <param name="callbackParameterName"></param>
        /// <param name="callbackParameterValue"></param>
        public JsonpFormatter(MediaTypeFormatter jsonMediaTypeFormatter, 
                              HttpRequestMessage request, 
                              string callbackParameterName, 
                              string callbackParameterValue)
            : this(jsonMediaTypeFormatter, callbackParameterName)
        {
            if (request == null)
            {
                throw new ArgumentNullException("HttpRequestMessage cannot be null");
            }

            if (string.IsNullOrEmpty(callbackParameterValue))
            {
                throw new ArgumentNullException("Callback parameter value cannot be empty");
            }
            
            this.request = request;
            this.callbackParameterValue = callbackParameterValue;
        }

        /// <summary>
        /// Gets the request formatter instance, either returns the default JsonMediaTypeFormatter
        /// if the request is asking for jsonp or returns this class for jsonp requests.
        /// </summary>
        /// <param name="type">Type of object to serialize</param>
        /// <param name="request">Current HttpRequestMessage</param>
        /// <param name="mediaType">Media type</param>
        /// <returns></returns>
        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, 
                                                                          HttpRequestMessage request, 
                                                                          MediaTypeHeaderValue mediaType)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Object being serialized must have a type");
            }

            if (request == null)
            {
                throw new ArgumentNullException("Request cannot be null");
            }

            string callbackValue;
            if (IsJsonpRequest(request, callbackParameterName, out callbackValue))
            {
                return new JsonpFormatter(jsonMediaTypeFormatter, request, callbackParameterName, callbackValue);
            }

            if (jsonMediaTypeFormatter != null)
            {
                return jsonMediaTypeFormatter;
            }

            return base.GetPerRequestFormatterInstance(type, request, mediaType);
        }

        /// <summary>
        /// Serialize and write the object to the stream.
        /// </summary>
        /// <param name="type">Type of object</param>
        /// <param name="value">Object value</param>
        /// <param name="writeStream">Output stream</param>
        /// <param name="content">Http content (headers etc)</param>
        /// <param name="transportContext">Transport context</param>
        /// <returns></returns>
        public override async Task WriteToStreamAsync(Type type, 
                                                object value, 
                                                Stream writeStream, 
                                                HttpContent content, 
                                                TransportContext transportContext)
        {

            if (type == null)
            {
                throw new ArgumentNullException("Type of object to write cannot be null");
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException("Stream cannot be null");
            }

            var encoding = SelectCharacterEncoding(content == null ? null : content.Headers);
            using (var writer = new StreamWriter(writeStream, encoding, 4096, true)) 
            {
                writer.Write(callbackParameterValue + "(");
                writer.Flush();
                await jsonMediaTypeFormatter.WriteToStreamAsync(type, value, writeStream, content, transportContext);
                writer.Write(");");
                writer.Flush();
            }
        }

        /// <summary>
        /// Dont need to read jsonp 
        /// </summary>
        /// <param name="type">Type to read</param>
        /// <return>bool can or cant read the type</returns>
        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <summary>
        /// Can we write the type to json
        /// </summary>
        /// <param name="type">Type of object to write</param>
        /// <returns>bool can or cant write the object</returns>
        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Type cannot be null");
            }
            return jsonMediaTypeFormatter.CanWriteType(type);
        }

        /// <summary>
        /// Checks if the current HttpRequestMessage is asking for jsonp
        /// or regular json
        /// </summary>
        /// <param name="request">Current request</param>
        /// <param name="callbackParameterName">Callback parameter name</param>
        /// <param name="callbackParameterValue">Callback parameter value</param>
        /// <returns></returns>
        private static bool IsJsonpRequest(HttpRequestMessage request, string callbackParameterName, out string callbackParameterValue)
        {
            callbackParameterValue = null;
            
            if (request == null || request.Method != HttpMethod.Get)
            {
                return false;
            }

            var fmtParameterValue = request.GetQueryNameValuePairs()
                .Where(kvp => kvp.Key.Equals("fmt", StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Value)
                .FirstOrDefault();

            callbackParameterValue = request.GetQueryNameValuePairs()
                .Where(kvp => kvp.Key.Equals(callbackParameterName, StringComparison.OrdinalIgnoreCase))
                .Select(s => s.Value)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(fmtParameterValue) && 
                fmtParameterValue.Equals("jsonp", StringComparison.OrdinalIgnoreCase))
            {
                callbackParameterValue = string.IsNullOrEmpty(callbackParameterValue)
                    ? DateTime.UtcNow.Ticks.ToString()
                    : callbackParameterValue;
                return true;
            }

            return !string.IsNullOrEmpty(callbackParameterValue);
        }
    }
}