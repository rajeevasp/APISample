using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace API.Infrastructure.Formatters
{
    /// <summary>
    /// Formatter to provide KML files
    /// </summary>
    public class KMLFormatter : MediaTypeFormatter
    {
        private UTF8Encoding encoder;
        /// <summary>
        /// Initialize a new instance of the <see cref="KMLFormatter"/> class and 
        /// set the media type and add a querystring mapping.
        /// </summary>
        public KMLFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/xml"));
            this.MediaTypeMappings.Add(new QueryStringMapping("fmt", "kml", "application/vnd.google-earth.kml+xml"));
            encoder = new UTF8Encoding(false, true);
            //this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.google-earth.kml+xml"));
            //this.MediaTypeMappings.Add(new QueryStringMapping("fmt", "kml", "application/vnd.google-earth.kml+xml"));
        }

        /// <summary>
        /// We aren't reading any incoming KML currently.
        /// </summary>
        /// <param name="type">KML Object</param>
        /// <returns></returns>
        public override bool CanReadType(Type type)
        {
            return false;
        }

        /// <summary>
        /// Check if the type is a valid KML object
        /// </summary>
        /// <param name="type">Type of object to write</param>
        /// <returns>bool can write type</returns>
        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Object type cannot be null");
            }

            return type == typeof(Kml);
        }

        /// <summary>
        /// Write the value out as KML to the stream.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="value">Value to write</param>
        /// <param name="writeStream">Stream to write to</param>
        /// <param name="content">Http content</param>
        /// <param name="transportContext">Transport channel binding info</param>
        /// <returns>Task</returns>
        public override Task WriteToStreamAsync(Type type, 
                                                object value, 
                                                Stream writeStream, 
                                                HttpContent content, 
                                                TransportContext transportContext)
        {
            Serializer serializer = new Serializer();
            serializer.SerializeRaw((Kml)value);
            return Task.Factory.StartNew(() => 
            {
                using (var writer = XmlWriter.Create(writeStream))
                {
                    writer.WriteRaw(serializer.Xml);
                }
            });
        }
    }
}